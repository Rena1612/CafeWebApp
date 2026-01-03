using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWebApp.Models
{
    /// <summary>
    /// Cart item model for shopping cart functionality
    /// </summary>
    public class CartItem
    {
        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Subtotal")]
        [NotMapped]
        public decimal Subtotal => Price * Quantity;
    }

    /// <summary>
    /// Shopping cart model
    /// </summary>
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal Total => Items.Sum(i => i.Subtotal);

        public int TotalItems => Items.Sum(i => i.Quantity);

        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    Items.Remove(item);
                }
            }
        }

        public void RemoveItem(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
