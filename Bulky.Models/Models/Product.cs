
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = default!;
        [Required]
        public string Description { get; set; } = default!;
        [Required]
        public string ISBN { get; set; } = default!;
        [Required]
        public string Author { get; set; } = default!;
        [Required]
        [Display(Name ="List Price")]
        [Range(1,1000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name ="Price for 1-50")]
        [Range(1,1000)]
        public double Price { get; set; }
        [Required]
        [Display(Name ="Price for 50+")]
        [Range(1,1000)]
        public double Price50 { get; set; }
        [Required]
        [Display(Name ="Price for 100+")]
        [Range(1,1000)]
        public double Price100 { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public string ImageUrl { get; set; } = "";


    }
}
