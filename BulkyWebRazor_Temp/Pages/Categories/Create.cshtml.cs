using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class CreateModel(ApplicationDbContext context) : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            context.Categories.Add(Category);
            await context.SaveChangesAsync();
            return RedirectToPage("Index");

        }
    }
}
