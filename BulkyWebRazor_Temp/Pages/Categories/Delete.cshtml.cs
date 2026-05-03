using BulkyBookWebRazor_Temp.Data;
using BulkyBookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBookWebRazor_Temp.Pages.Categories
{
    public class DeleteModel(ApplicationDbContext context) : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet(int? CategoryId)
        {
            if(CategoryId != null && CategoryId != 0)
            {
                Category = context.Categories.Find(CategoryId);
            }

        }
        public async Task<IActionResult> OnPost()
        {
            Category? obj = await context.Categories.FindAsync(Category.CategoryId);
            if (obj == null) return NotFound();
        
                context.Categories.Remove(obj);
               await context.SaveChangesAsync();
                return  RedirectToPage("Index");
            
            
        }

    }
}
