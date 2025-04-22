using Backend.Models;

namespace Backend.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Prevent duplicate seeding
            if (context.Users.Any() || context.Products.Any()) return;

            // Create users with different roles
            var backendEmployee = new User
            {
                Id = Guid.NewGuid(),
                Name = "Bob",
                Email = "bob@backend.com",
                Password = "securepassword123",
                Role = UserRole.BackendEmployee
            };

            var productSeller = new User
            {
                Id = Guid.NewGuid(),
                Name = "Alice",
                Email = "alice@example.com",
                Password = "securepassword123",
                Role = UserRole.ProductSeller
            };

            var customer = new User
            {
                Id = Guid.NewGuid(),
                Name = "Charlie",
                Email = "charlie@customer.com",
                Password = "securepassword123",
                Role = UserRole.Customer
            };

            // Create products linked to the ProductSeller
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Laptop",
                    Type = Types.Electronics,
                    Description = "Portable computer",
                    Price = 999.99,
                    UserId = productSeller.Id
                },
                new Product
                {
                    Name = "Notebook",
                    Type = Types.Books,
                    Description = "For writing notes",
                    Price = 5.49,
                    UserId = productSeller.Id
                },
                new Product
                {
                    Name = "Water Bottle",
                    Type = Types.Groceries,
                    Description = "Keeps drinks cold",
                    Price = 19.99,
                    UserId = productSeller.Id
                }
            };

            // Add users and products to the context
            context.Users.AddRange(backendEmployee, productSeller, customer);
            context.Products.AddRange(products);

            context.SaveChanges();
        }
    }
}
