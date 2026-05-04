using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BulkyBookWeb.NTier.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Product.GetAllAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(int? productId) //Update and Insert
        {
            var categories = await _unitOfWork.Category.GetAllAsync();
            IEnumerable<SelectListItem> CategoryList = categories.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            ProductViewModel productVm = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if(productId ==null || productId == 0)
            {
                //Create 
                return View(productVm);
            }else
            {
                //Update 
                productVm.Product = await _unitOfWork.Product.GetAsync(u => u.Id == productId);
                return View(productVm);
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> Edit(int? productId)
        //{
        //    if (productId is null || productId == 0)
        //        return NotFound();

        //    var product = await _unitOfWork.Product.GetAsync(c => c.Id == productId);
        //    if (product == null) return NotFound();

        //    return View(product);
        //}
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
        public async Task<IActionResult> Upsert(ProductViewModel productVM, IFormFile? file)
        {
            
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image 
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;

                }
                if (productVM.Product.Id == 0)
                {
                await _unitOfWork.Product.AddAsync(productVM.Product);

                }else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                await _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();

        }
        //[HttpPost]
        //public async Task<IActionResult> Edit(Product obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //       _unitOfWork.Product.Update(obj);
        //        await _unitOfWork.Save();
        //        TempData["success"] = "Product updated successfully";
        //        return RedirectToAction("Index");


        //    }
        //    return View();

        //}
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
