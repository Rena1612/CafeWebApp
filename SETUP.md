# Quick Setup Guide

## Step 1: Open Solution
1. Double-click `CafeWebApp.sln`
2. Wait for Visual Studio to load

## Step 2: Restore Packages
Visual Studio will automatically restore NuGet packages.
If not, right-click solution → Restore NuGet Packages

## Step 3: Create Database
Open Package Manager Console (Tools → NuGet Package Manager → Package Manager Console)
Run these commands:

```powershell
Add-Migration InitialCreate
Update-Database
```

This will create the database and seed it with sample data.

## Step 4: Run Application
Press F5 or click the green Run button

## Login Credentials

### Admin
- Email: admin@cafe.com
- Password: Admin@123
- Access: Admin Panel + All Customer Features

### Customer  
- Email: customer@example.com
- Password: Customer@123
- Access: Menu, Cart, Checkout, Order History

## Quick Tour

### Customer Journey
1. Go to Menu → Browse products by category
2. Add items to cart
3. View cart → Update quantities
4. Checkout → Fill form and place order
5. View "My Orders" to see order history

### Admin Journey
1. Login as admin
2. Click "Admin Panel" in navbar
3. Dashboard → View statistics
4. Products → Add/Edit/Delete products
5. Orders → View and update order status

## Sample Data Included

- 5 Categories (Coffee, Tea, Pastries, Sandwiches, Desserts)
- 18 Products with prices and descriptions
- 2 User accounts (Admin & Customer)

## Troubleshooting

**Database Error?**
- Ensure SQL Server LocalDB is installed
- Run Update-Database again

**Can't Login?**
- Database might not be seeded
- Delete database and run Update-Database again

**Port Already In Use?**
- Change port in Properties/launchSettings.json

## Technology Stack

- ASP.NET Core 8.0 MVC
- Entity Framework Core
- SQL Server LocalDB
- Bootstrap 5
- ASP.NET Identity

Enjoy exploring the Cafe Web Application!
