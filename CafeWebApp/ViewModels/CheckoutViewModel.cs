using System.ComponentModel.DataAnnotations;

namespace CafeWebApp.ViewModels
{
    /// <summary>
    /// View model for checkout process
    /// </summary>
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Full Name")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Table Number")]
        [StringLength(10)]
        public string? TableNumber { get; set; }

        [Display(Name = "Takeaway Order")]
        public bool IsTakeaway { get; set; }

        [Required(ErrorMessage = "Please select a payment method")]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } = string.Empty;

        [Display(Name = "Special Instructions")]
        [StringLength(500)]
        public string? Notes { get; set; }

        // Cart summary (for display)
        public decimal TotalAmount { get; set; }
        public int TotalItems { get; set; }
    }
}
