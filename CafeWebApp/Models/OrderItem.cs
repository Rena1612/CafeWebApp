using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWebApp.Models
{
    /// <summary>
    /// Order item model representing individual items in an order
    /// </summary>
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order")]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Unit Price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        [Display(Name = "Subtotal")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal => UnitPrice * Quantity;

        // Navigation properties
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
