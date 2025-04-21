using Backend.Models;

namespace Backend.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Prevent duplicate seeding
            if (context.Users.Any() || context.Products.Any()) return;

            // Create a user
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Alice",
                Email = "alice@example.com",
                Password = "securepassword123"
            };

            // Create products linked to that user
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Laptop",
                    Type = Types.Electronics,
                    Description = "Portable computer",
                    Price = 999.99,
                    UserId = user.Id
                },
                new Product
                {
                    Name = "Notebook",
                    Type = Types.Books,
                    Description = "For writing notes",
                    Price = 5.49,
                    UserId = user.Id
                },
                new Product
                {
                    Name = "Water Bottle",
                    Type = Types.Groceries,
                    Description = "Keeps drinks cold",
                    Price = 19.99,
                    UserId = user.Id
                }
            };

            // Add user and products
            context.Users.Add(user);
            context.Products.AddRange(products);

            context.SaveChanges();
        }
    }
}
