using Book.DataAccessLayer.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModels;
using Book.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Security.Claims;

namespace BookVerse.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
        
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new OrderHeader()
            };

            foreach (var cart in shoppingCartViewModel.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                shoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(shoppingCartViewModel);
        }

        public IActionResult Summary() 
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new OrderHeader()
            };

            ShoppingCartViewModel.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            ShoppingCartViewModel.OrderHeader.Name = ShoppingCartViewModel.OrderHeader.ApplicationUser.Name;
            ShoppingCartViewModel.OrderHeader.PhoneNumber = ShoppingCartViewModel.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartViewModel.OrderHeader.StreetAddress = ShoppingCartViewModel.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartViewModel.OrderHeader.City = ShoppingCartViewModel.OrderHeader.ApplicationUser.City;
            ShoppingCartViewModel.OrderHeader.State = ShoppingCartViewModel.OrderHeader.ApplicationUser.State;
            ShoppingCartViewModel.OrderHeader.PostalCode = ShoppingCartViewModel.OrderHeader.ApplicationUser.PostalCode;

            foreach (var cart in ShoppingCartViewModel.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartViewModel);
        }

        [HttpPost]
        [ActionName("Summary")]
		public IActionResult SummaryPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartViewModel.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product");

			ShoppingCartViewModel.OrderHeader.OrderDate = DateTime.Now;
			ShoppingCartViewModel.OrderHeader.ApplicationUserId = userId;
			
            ApplicationUser applicationUser= _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

			foreach (var cart in ShoppingCartViewModel.ShoppingCartList)
			{
				cart.Price = GetPriceBasedOnQuantity(cart);
				ShoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}

            if(applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                //Regular Customer Account
                ShoppingCartViewModel.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
                ShoppingCartViewModel.OrderHeader.OrderStatus = SD.StatusPending;
            }
            else
            {
				//Company User
				ShoppingCartViewModel.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
				ShoppingCartViewModel.OrderHeader.OrderStatus = SD.StatusApproved;
			}

            _unitOfWork.OrderHeader.Add(ShoppingCartViewModel.OrderHeader);
            _unitOfWork.Complete();

            foreach(var cart in ShoppingCartViewModel.ShoppingCartList)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartViewModel.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _unitOfWork.OrderDetail.Add(orderDetails);
                _unitOfWork.Complete();
            }

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                //Stripe
                var domain = "https://localhost:7017/";
                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={ShoppingCartViewModel.OrderHeader.Id}",
                    CancelUrl = domain + "customer/cart/Index",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };

                foreach(var item in ShoppingCartViewModel.ShoppingCartList)
                {
                    var sessionLineItemOptions = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price*100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Title
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItemOptions);
                };

                var service = new SessionService();
                Session session = service.Create(options);

                _unitOfWork.OrderHeader.UpdateStripePaymentId(ShoppingCartViewModel.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Complete();

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }

			return RedirectToAction(nameof(OrderConfirmation), new {id = ShoppingCartViewModel.OrderHeader.Id});
		}

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == id, includeProperties: "ApplicationUser");
            if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                //check the stripe status
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStripePaymentId(id, orderHeader.SessionId, session.PaymentIntentId);
                    _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                    _unitOfWork.Complete();
                }
            }
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId ==orderHeader.ApplicationUserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Complete();

            return View(id);
        }

		private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if(shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.BulkPrice;
            }
            else
            {
                if (shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.BulkPriceFifty;
                }
                else
                {
                    return shoppingCart.Product.BulkPriceHundred;
                }
            }
        }

        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
            cart.Count += 1;
            _unitOfWork.ShoppingCart.Update(cart);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);

            if (cart.Count == 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Complete();
            }
            else
            {
                cart.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cart);
                _unitOfWork.Complete();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get (c => c.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}
