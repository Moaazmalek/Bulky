using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.NTier.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> productList =await  _unitOfWork.Product.GetAllAsync(includeProperties:"Category");

            return View(productList);
        }
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _unitOfWork.Product.GetAsync(p => p.Id == id, includeProperties: "Category");

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
