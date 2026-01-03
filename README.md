# Cafe Web Application - ASP.NET MVC

A full-featured cafe management web application built with ASP.NET Core MVC, Entity Framework, and SQL Server.

## Features

### Customer Features
- Browse menu by categories
- View product details with images
- Add items to shopping cart
- Update cart quantities
- Checkout with order details
- View order history
- User registration and authentication

### Admin Features
- Dashboard with statistics
- CRUD operations for Categories
- CRUD operations for Products (with image URL management)
- View and manage orders
- Update order status (Pending → Preparing → Completed)
- View order details

## Technologies Used

- **Framework**: ASP.NET Core 8.0 MVC
- **ORM**: Entity Framework Core
- **Database**: SQL Server (LocalDB)
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Bootstrap 5, Bootstrap Icons, jQuery
- **Architecture**: Repository & Service Pattern

## Project Structure

```
CafeWebApp/
├── Models/                 # Domain models
│   ├── ApplicationUser.cs
│   ├── Category.cs
│   ├── Product.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   └── CartItem.cs
├── ViewModels/            # View models for forms
│   ├── LoginViewModel.cs
│   ├── RegisterViewModel.cs
│   └── CheckoutViewModel.cs
├── Data/                  # Database context and seeding
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
├── Repositories/          # Repository pattern implementation
│   ├── ICategoryRepository.cs
│   ├── CategoryRepository.cs
│   ├── IProductRepository.cs
│   ├── ProductRepository.cs
│   ├── IOrderRepository.cs
│   └── OrderRepository.cs
├── Services/              # Business logic services
│   ├── ICartService.cs
│   ├── CartService.cs
│   ├── IOrderService.cs
│   └── OrderService.cs
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs
│   ├── MenuController.cs
│   ├── CartController.cs
│   ├── CheckoutController.cs
│   └── AccountController.cs
├── Areas/Admin/           # Admin area
│   ├── Controllers/
│   │   ├── DashboardController.cs
│   │   ├── CategoriesController.cs
│   │   ├── ProductsController.cs
│   │   └── OrdersController.cs
│   └── Views/
└── Views/                 # Razor views
```

## Database Schema

### Tables
- **AspNetUsers** - User accounts (Identity)
- **AspNetRoles** - User roles (Admin, Customer)
- **Categories** - Product categories
- **Products** - Menu items
- **Orders** - Customer orders
- **OrderItems** - Individual items in orders

## Setup Instructions

### Prerequisites
- Visual Studio 2022 or later
- .NET 8.0 SDK
- SQL Server 2019 or SQL Server LocalDB

### Installation Steps

1. **Extract the ZIP file** to your desired location

2. **Open the solution** in Visual Studio
   - Double-click `CafeWebApp.sln`

3. **Update Connection String** (Optional)
   - Open `appsettings.json`
   - Modify the connection string if needed:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CafeDB;Trusted_Connection=true"
   }
   ```

4. **Restore NuGet Packages**
   - Visual Studio will automatically restore packages
   - Or run: `dotnet restore`

5. **Apply Database Migrations**
   - Open Package Manager Console (Tools → NuGet Package Manager → Package Manager Console)
   - Run the following commands:
   ```
   Add-Migration InitialCreate
   Update-Database
   ```

6. **Run the Application**
   - Press F5 or click the Run button
   - The database will be automatically seeded with sample data

## Default Credentials

### Admin Account
- **Email**: admin@cafe.com
- **Password**: Admin@123

### Customer Account
- **Email**: customer@example.com
- **Password**: Customer@123

## Sample Data

The application seeds the database with:
- 5 Categories (Coffee, Tea, Pastries, Sandwiches, Desserts)
- 18 Sample Products across all categories
- 2 User accounts (Admin and Customer)

## Usage Guide

### For Customers

1. **Browse Menu**
   - Navigate to Menu page
   - Filter by category
   - View product details

2. **Add to Cart**
   - Click "Add to Cart" on any product
   - View cart icon badge for item count

3. **Checkout**
   - Review cart items
   - Login or register
   - Fill checkout form
   - Place order

4. **View Orders**
   - Navigate to "My Orders"
   - View order history and status

### For Administrators

1. **Access Admin Panel**
   - Login with admin credentials
   - Click "Admin Panel" in navigation

2. **Manage Categories**
   - Create, edit, or delete categories
   - Set display order and active status

3. **Manage Products**
   - Add new products with details
   - Set prices, categories, and stock status
   - Update product information
   - Mark products as featured

4. **Manage Orders**
   - View all orders
   - Filter by status
   - Update order status
   - View order details

## Configuration

### Session Timeout
Default: 30 minutes (configured in Program.cs)

### Password Requirements
- Minimum length: 6 characters
- Requires: uppercase, lowercase, and digit
- No special character required

### Cart Storage
Shopping cart uses session storage (in-memory)

## Key Features Explained

### Repository Pattern
Separates data access logic from business logic for better testability and maintainability.

### Service Layer
Encapsulates business logic and orchestrates operations between repositories.

### Session-based Cart
Shopping cart persists during the user's session using JSON serialization.

### Bootstrap UI
Responsive design that works on mobile, tablet, and desktop devices.

### Role-based Authorization
- Customer role: Browse, cart, checkout, view own orders
- Admin role: Full CRUD access to all entities

## Product Image Management

Products support image URLs. To add images:
1. Place images in `wwwroot/images/products/`
2. Enter the path in ImageUrl field: `/images/products/yourimage.jpg`
3. Or use external URLs

Note: This is a simple path-based approach. For production, consider implementing file upload functionality.

## Order Flow

```
Customer adds items to cart
    ↓
Proceeds to checkout
    ↓
Fills order details
    ↓
Places order (Status: Pending)
    ↓
Admin views order
    ↓
Updates status to Preparing
    ↓
Updates status to Ready/Completed
```

## Troubleshooting

### Database Connection Issues
- Ensure SQL Server LocalDB is installed
- Check connection string in appsettings.json
- Verify database exists: `Update-Database` in Package Manager Console

### Login Issues
- Ensure database is seeded with default users
- Use provided demo credentials
- Check if Identity tables are created

### Missing Packages
- Run `dotnet restore` in project directory
- Rebuild solution

## Future Enhancements

Potential improvements for the application:
- File upload for product images
- Order notifications
- Payment gateway integration
- Reviews and ratings
- Inventory management
- Sales reports and analytics
- Email confirmations
- Real-time order tracking
- Multiple locations support

## License

This is a demo application for educational purposes.

## Support

For issues or questions, please review the code documentation.

---

**Developed with ASP.NET Core MVC**
**Version: 1.0**
**Last Updated: 2026**
