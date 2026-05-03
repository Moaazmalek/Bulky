
using BulkyBookWebRazor_Temp.Data;
using BulkyBookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBookWebRazor_Temp.Pages.Categories
{
    public class IndexModel(ApplicationDbContext context) : PageModel
    {
        public List<Category> CategoryList { get; set; }
       
        public void OnGet()
        {
            CategoryList = [.. context.Categories];
        }
    }
}
