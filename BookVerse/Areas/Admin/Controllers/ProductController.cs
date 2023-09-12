﻿using Book.DataAccessLayer.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModels;
using BooK.DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookVerse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> categories = _unitOfWork.Product.GetAll().ToList();

            return View(categories);
        }

        public IActionResult Upsert(int? id)
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

            if (id == null || id == 0)
            {
                return View(productViewModel);
            }
            else
            {
                productViewModel.Products = _unitOfWork.Product.Get(x => x.Id == id);
                return View(productViewModel);
            }

        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel product, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product.Products);
                _unitOfWork.Complete();
                TempData["success"] = "Product Created successfully!";
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
