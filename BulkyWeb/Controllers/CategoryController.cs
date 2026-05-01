using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();

            return View(categories);
        }
        [HttpGet]
        public IActionResult  Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? categoryId)
        {
            if (categoryId is null || categoryId == 0)
                return NotFound();

            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) return NotFound();

            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? categoryId)
        {
            if (categoryId is null || categoryId == 0)
                return NotFound();

            var category = await _context.Categories.FindAsync(categoryId);
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
            await _context.Categories.AddAsync(obj);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index","Category");
            }
            return View();
         
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                await _context.SaveChangesAsync();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");


            }
            return View();
         
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? categoryId)
        {
            Category? obj = await _context.Categories.FindAsync(categoryId);
            if(obj == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(obj);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Deleted successfully";

            return RedirectToAction("Index");
         
        }
        
    }
}
