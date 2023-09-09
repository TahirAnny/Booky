using Book.DataAccessLayer.Repository.IRepository;
using Book.Models;
using BooK.DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookVerse.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _category;

        public CategoryController(ICategoryRepository category)
        {
            _category = category;
        }

        public IActionResult Index()
        {
            List<Category> categories = _category.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _category.Add(category);
                _category.Save();
                TempData["success"] = "Category Created successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? categoryId)
        {
            if(categoryId == null || categoryId == 0)
            {
                return NotFound();
            }
            Category? category = _category.Get(c => c.Id == categoryId);

            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _category.Update(category);
                _category.Save();
                TempData["success"] = "Category Edited successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? categoryId)
        {
            if (categoryId == null || categoryId == 0)
            {
                return NotFound();
            }
            Category? category = _category.Get(c => c.Id == categoryId);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteData(int? categoryId)
        {
            Category? category = _category.Get(c => c.Id == categoryId);
            if (category == null)
            {
                return NotFound();
            }
            _category.Remove(category);
            _category.Save();
            TempData["success"] = "Category Deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
