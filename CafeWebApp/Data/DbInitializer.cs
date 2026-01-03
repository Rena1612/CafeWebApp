using CafeWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CafeWebApp.Data
{
    /// <summary>
    /// Database initializer for seeding data
    /// </summary>
    public static class DbInitializer
    {
        public static async Task SeedAsync(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Ensure database is created
            await context.Database.MigrateAsync();

            // Seed Roles
            await SeedRolesAsync(roleManager);

            // Seed Admin User
            await SeedAdminUserAsync(userManager);

            // Seed Categories and Products
            await SeedCategoriesAndProductsAsync(context);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            // Create admin user
            var adminEmail = "admin@cafe.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "Admin User",
                    PhoneNumber = "1234567890",
                    RegisteredDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Create a sample customer user
            var customerEmail = "customer@example.com";
            var customerUser = await userManager.FindByEmailAsync(customerEmail);

            if (customerUser == null)
            {
                customerUser = new ApplicationUser
                {
                    UserName = customerEmail,
                    Email = customerEmail,
                    EmailConfirmed = true,
                    FullName = "John Doe",
                    PhoneNumber = "9876543210",
                    RegisteredDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(customerUser, "Customer@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customerUser, "Customer");
                }
            }
        }

        private static async Task SeedCategoriesAndProductsAsync(ApplicationDbContext context)
        {
            // Check if data already exists
            if (await context.Categories.AnyAsync())
            {
                return; // Database has been seeded
            }

            // Seed Categories
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Coffee",
                    Description = "Hot and cold coffee beverages",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new Category
                {
                    Name = "Tea",
                    Description = "Premium tea selection",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new Category
                {
                    Name = "Pastries",
                    Description = "Fresh baked goods",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new Category
                {
                    Name = "Sandwiches",
                    Description = "Delicious sandwiches and wraps",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new Category
                {
                    Name = "Desserts",
                    Description = "Sweet treats and desserts",
                    DisplayOrder = 5,
                    IsActive = true
                }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            // Seed Products
            var products = new List<Product>
            {
                // Coffee
                new Product
                {
                    Name = "Espresso",
                    Description = "Rich and bold single shot of espresso",
                    Price = 2.50m,
                    CategoryId = categories[0].Id,
                    ImageUrl = "/images/products/espresso.jpg",
                    InStock = true,
                    IsFeatured = true
                },
                new Product
                {
                    Name = "Cappuccino",
                    Description = "Espresso with steamed milk and foam",
                    Price = 4.50m,
                    CategoryId = categories[0].Id,
                    ImageUrl = "/images/products/cappuccino.jpg",
                    InStock = true,
                    IsFeatured = true
                },
                new Product
                {
                    Name = "Latte",
                    Description = "Smooth espresso with steamed milk",
                    Price = 4.75m,
                    CategoryId = categories[0].Id,
                    ImageUrl = "/images/products/latte.jpg",
                    InStock = true
                },
                new Product
                {
                    Name = "Americano",
                    Description = "Espresso with hot water",
                    Price = 3.50m,
                    CategoryId = categories[0].Id,
                    ImageUrl = "/images/products/americano.jpg",
                    InStock = true
                },
                new Product
                {
                    Name = "Mocha",
                    Description = "Espresso with chocolate and steamed milk",
                    Price = 5.25m,
                    CategoryId = categories[0].Id,
                    ImageUrl = "/images/products/mocha.jpg",
                    InStock = true,
                    IsFeatured = true
                },
                new Product
                {
                    Name = "Iced Coffee",
                    Description = "Cold brewed coffee over ice",
                    Price = 4.00m,
                    CategoryId = categories[0].Id,
                    ImageUrl = "/images/products/iced-coffee.jpg",
                    InStock = true
                },

                // Tea
                new Product
                {
                    Name = "English Breakfast Tea",
                    Description = "Classic black tea blend",
                    Price = 3.00m,
                    CategoryId = categories[1].Id,
                    ImageUrl = "/images/products/english-breakfast.jpg",
                    InStock = true
                },
                new Product
                {
                    Name = "Green Tea",
                    Description = "Organic Japanese green tea",
                    Price = 3.25m,
                    CategoryId = categories[1].Id,
                    ImageUrl = "/images/products/green-tea.jpg",
                    InStock = true
                },
                new Product
                {
                    Name = "Chamomile Tea",
                    Description = "Soothing herbal tea",
                    Price = 3.50m,
                    CategoryId = categories[1].Id,
                    ImageUrl = "/images/products/chamomile.jpg",
                    InStock = true
                },

                // Pastries
                new Product
                {
                    Name = "Croissant",
                    Description = "Buttery, flaky French pastry",
                    Price = 3.75m,
                    CategoryId = categories[2].Id,
                    ImageUrl = "/images/products/croissant.jpg",
                    InStock = true,
                    IsFeatured = true
                },
                new Product
                {
                    Name = "Chocolate Muffin",
                    Description = "Rich chocolate chip muffin",
                    Price = 3.50m,
                    CategoryId = categories[2].Id,
                    ImageUrl = "/images/products/chocolate-muffin.jpg",
                    InStock = true
                },
                new Product
                {
                    Name = "Blueberry Scone",
                    Description = "Fresh baked scone with blueberries",
                    Price = 3.25m,
                    CategoryId = categories[2].Id,
                    ImageUrl = "/images/products/blueberry-scone.jpg",
                    InStock = true
                },

                // Sandwiches
                new Product
                {
                    Name = "Club Sandwich",
                    Description = "Triple decker with turkey, bacon, lettuce, and tomato",
                    Price = 8.50m,
                    CategoryId = categories[3].Id,
                    ImageUrl = "/images/products/club-sandwich.jpg",
                    InStock = true
                },
                new Product
                {
                    Name = "Veggie Wrap",
                    Description = "Fresh vegetables in a whole wheat wrap",
                    Price = 7.25m,
                    CategoryId = categories[3].Id,
                    ImageUrl = "/images/products/veggie-wrap.jpg",
                    InStock = true
                },
                new Product
                {
                    Name = "Chicken Panini",
                    Description = "Grilled chicken with pesto and mozzarella",
                    Price = 8.75m,
                    CategoryId = categories[3].Id,
                    ImageUrl = "/images/products/chicken-panini.jpg",
                    InStock = true,
                    IsFeatured = true
                },

                // Desserts
                new Product
                {
                    Name = "Cheesecake",
                    Description = "New York style cheesecake",
                    Price = 5.50m,
                    CategoryId = categories[4].Id,
                    ImageUrl = "/images/products/cheesecake.jpg",
                    InStock = true,
                    IsFeatured = true
                },
                new Product
                {
                    Name = "Chocolate Brownie",
                    Description = "Fudgy chocolate brownie",
                    Price = 4.25m,
                    CategoryId = categories[4].Id,
                    ImageUrl = "/images/products/brownie.jpg",
                    InStock = true
                },
                new Product
                {
                    Name = "Tiramisu",
                    Description = "Classic Italian coffee-flavored dessert",
                    Price = 6.00m,
                    CategoryId = categories[4].Id,
                    ImageUrl = "/images/products/tiramisu.jpg",
                    InStock = true
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
