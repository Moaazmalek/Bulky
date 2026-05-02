using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.NTier.Controllers { 
    public class CategoryController(ICategoryRepository context) : Controller
    {
        private readonly ICategoryRepository categoryRepo = context;
        public async Task<IActionResult> Index()
        {
            var categories = await categoryRepo.GetAllAsync();

            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? categoryId)
        {
            if (categoryId is null || categoryId == 0)
                return NotFound();

            var category = await categoryRepo.GetAsync(c => c.CategoryId == categoryId);
            if (category == null) return NotFound();

            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? categoryId)
        {
            if (categoryId is null || categoryId == 0)
                return NotFound();

            var category = await categoryRepo.GetAsync(u => u.CategoryId == categoryId);
            if (category == null) return NotFound();

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category obj)
        {
            if (obj.Name.ToLower() == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder can't match the Name.");
            }
            if (ModelState.IsValid)
            {
                await categoryRepo.AddAsync(obj);
                await categoryRepo.SaveAsync();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
               categoryRepo.Update(obj);
                await categoryRepo.SaveAsync();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");


            }
            return View();

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? categoryId)
        {
            Category? obj = await categoryRepo.GetAsync(u => u.CategoryId == categoryId);
            if (obj == null)
            {
                return NotFound();
            }
            categoryRepo.Remove(obj);
            await categoryRepo.SaveAsync();
            TempData["success"] = "Category Deleted successfully";

            return RedirectToAction("Index");

        }

    }
}
