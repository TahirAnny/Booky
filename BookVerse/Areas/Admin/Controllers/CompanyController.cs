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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  
        }

        public IActionResult Index()
        {
            List<Company> categories = _unitOfWork.Company.GetAll().ToList();

            return View(categories);
        }

        public IActionResult Upsert(int? CompanyId)
        {
            if (CompanyId == null || CompanyId == 0)
            {
                return View(new Company());
            }
            else
            {
                Company company = _unitOfWork.Company.Get(x => x.Id == CompanyId);
                return View(company);
            }

        }

        [HttpPost]
        public IActionResult Upsert(Company Company, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                if(Company.Id == 0)
                {
                    _unitOfWork.Company.Add(Company);
                }
                else
                {
                    _unitOfWork.Company.Update(Company);
                }

                _unitOfWork.Complete();
                TempData["success"] = "Company Saved successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(new Company());
            }
        }

        public IActionResult Delete(int? CompanyId)
        {
            if (CompanyId == null || CompanyId == 0)
            {
                return NotFound();
            }
            Company? Company = _unitOfWork.Company.Get(c => c.Id == CompanyId);

            if (Company == null)
            {
                return NotFound();
            }
            return View(Company);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteData(int? CompanyId)
        {
            Company? Company = _unitOfWork.Company.Get(c => c.Id == CompanyId);
            if (Company == null)
            {
                return NotFound();
            }
            _unitOfWork.Company.Remove(Company);
            _unitOfWork.Complete();
            TempData["success"] = "Company Deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
