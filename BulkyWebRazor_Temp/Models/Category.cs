using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWebRazor_Temp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; } = default!;
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100 ")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
