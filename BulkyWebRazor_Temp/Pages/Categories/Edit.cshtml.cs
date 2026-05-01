using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class EditModel(ApplicationDbContext context) : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet(int? CategoryId)
        {
            if(CategoryId != null && CategoryId !=0)
            {
                Category = context.Categories.Find(CategoryId);
            }
             
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                context.Categories.Update(Category);
                context.SaveChanges();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
