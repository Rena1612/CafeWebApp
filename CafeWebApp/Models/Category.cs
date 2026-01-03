using System.ComponentModel.DataAnnotations;

namespace CafeWebApp.Models
{
    /// <summary>
    /// Product category model
    /// </summary>
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [StringLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        // Navigation property
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
