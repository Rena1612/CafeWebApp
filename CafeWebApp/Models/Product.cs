using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWebApp.Models
{
    /// <summary>
    /// Product model representing cafe menu items
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        [StringLength(500)]
        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "In Stock")]
        public bool InStock { get; set; } = true;

        [Display(Name = "Featured")]
        public bool IsFeatured { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
