using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;


namespace BulkyBookWeb.NTier.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IUnitOfWork unitOfWork) : Controller
    {
      
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Product.GetAllAsync();

            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? productId)
        {
            if (productId is null || productId == 0)
                return NotFound();

            var product = await _unitOfWork.Product.GetAsync(c => c.Id == productId);
            if (product == null) return NotFound();

            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? productId)
        {
            if (productId is null || productId == 0)
                return NotFound();

            var product = await _unitOfWork.Product.GetAsync(u => u.Id == productId);
            if (product == null) return NotFound();

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product obj)
        {
            
            if (ModelState.IsValid)
            {
                await _unitOfWork.Product.AddAsync(obj);
                await _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
               _unitOfWork.Product.Update(obj);
                await _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");


            }
            return View();

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? productId)
        {
            Product? obj = await _unitOfWork.Product.GetAsync(u => u.Id == productId);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            await _unitOfWork.Save();
            TempData["success"] = "Product Deleted successfully";

            return RedirectToAction("Index");

        }

    }
}
