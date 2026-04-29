using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        
        public int CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public int DisplayOrder { get; set; }

    }
}
