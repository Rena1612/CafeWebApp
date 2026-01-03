using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWebApp.Models
{
    /// <summary>
    /// Order model representing customer orders
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public string CustomerId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(20)]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Table Number")]
        [StringLength(10)]
        public string? TableNumber { get; set; }

        [Display(Name = "Takeaway Order")]
        public bool IsTakeaway { get; set; }

        [Required]
        [Display(Name = "Total Amount")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Display(Name = "Order Status")]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [Display(Name = "Payment Method")]
        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [Display(Name = "Special Instructions")]
        [StringLength(500)]
        public string? Notes { get; set; }

        [Display(Name = "Order Date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Updated Date")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser? Customer { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    /// <summary>
    /// Order status constants
    /// </summary>
    public static class OrderStatus
    {
        public const string Pending = "Pending";
        public const string Preparing = "Preparing";
        public const string Ready = "Ready";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";
    }
}
