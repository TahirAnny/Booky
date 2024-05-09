using Book.DataAccessLayer.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModels;
using Book.Utility;
using BooK.DataAccessLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookVerse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;   
        }

        public IActionResult Index()
        {
            List<Product> categories = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();

            return View(categories);
        }

        public IActionResult Upsert(int? productId)
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.Id.ToString()
            });

            ProductViewModel productViewModel = new()
            {
                CagetoryList = categoryList,
                Products = new Product()
            };

            if (productId == null || productId == 0)
            {
                return View(productViewModel);
            }
            else
            {
                productViewModel.Products = _unitOfWork.Product.Get(x => x.Id == productId);
                return View(productViewModel);
            }

        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel product, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(formFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(product.Products.ImgUrl))
                    {
                        //delete old image
                        var oldImagePath = 
                            Path.Combine(wwwRootPath, product.Products.ImgUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        formFile.CopyTo(fileStream);
                    }

                    product.Products.ImgUrl = @"\images\product\" + fileName;
                }

                if(product.Products.Id == 0)
                {
                    _unitOfWork.Product.Add(product.Products);
                }
                else
                {
                    _unitOfWork.Product.Update(product.Products);
                }

                _unitOfWork.Complete();
                TempData["success"] = "Product Saved successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().ToList().Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                });

                product.CagetoryList = categoryList;

                return View(product);
            }
        }

        public IActionResult Delete(int? productId)
        {
            if (productId == null || productId == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.Product.Get(c => c.Id == productId);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteData(int? productId)
        {
            Product? product = _unitOfWork.Product.Get(c => c.Id == productId);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Complete();
            TempData["success"] = "Product Deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
