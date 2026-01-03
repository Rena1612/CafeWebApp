using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CafeWebApp.Models
{
    /// <summary>
    /// Application user model extending ASP.NET Identity
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name")]
        [StringLength(100)]
        public string? FullName { get; set; }

        [Display(Name = "Date Registered")]
        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        // Navigation property
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
