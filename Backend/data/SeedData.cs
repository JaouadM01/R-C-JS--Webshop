using Backend.Models;

namespace Backend.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Products.Any()) return;

            context.Products.AddRange(
                new Product { Name = "Laptop", Type = Types.Electronics, Description = "Portable computer", Price = 999.99 },
                new Product { Name = "Water Bottle", Type = Types.Groceries, Description = "Keeps drinks cold", Price = 19.99 },
                new Product { Name = "Notebook", Type = Types.Books, Description = "For writing notes", Price = 5.49 },
                new Product { Name = "Smartphone", Type = Types.Electronics, Description = "Touchscreen mobile device", Price = 799.99 },
                new Product { Name = "Headphones", Type = Types.Electronics, Description = "Noise-cancelling headphones", Price = 199.99 },
                new Product { Name = "Backpack", Type = Types.SportsEquipment, Description = "Durable travel backpack", Price = 49.99 },
                new Product { Name = "Desk Lamp", Type = Types.Furniture, Description = "LED desk lamp", Price = 29.99 },
                new Product { Name = "Gaming Chair", Type = Types.Furniture, Description = "Ergonomic gaming chair", Price = 249.99 },
                new Product { Name = "Mouse", Type = Types.Electronics, Description = "Wireless computer mouse", Price = 24.99 },
                new Product { Name = "Keyboard", Type = Types.Electronics, Description = "Mechanical keyboard", Price = 89.99 },
                new Product { Name = "Monitor", Type = Types.Electronics, Description = "4K UHD monitor", Price = 399.99 },
                new Product { Name = "Coffee Mug", Type = Types.Furniture, Description = "Ceramic coffee mug", Price = 12.99 },
                new Product { Name = "Pen", Type = Types.Books, Description = "Ballpoint pen", Price = 1.99 },
                new Product { Name = "Pencil", Type = Types.Books, Description = "Graphite pencil", Price = 0.99 },
                new Product { Name = "Eraser", Type = Types.Books, Description = "Rubber eraser", Price = 0.49 },
                new Product { Name = "Tablet", Type = Types.Electronics, Description = "10-inch tablet", Price = 499.99 },
                new Product { Name = "Smartwatch", Type = Types.Electronics, Description = "Fitness tracking smartwatch", Price = 199.99 },
                new Product { Name = "Sunglasses", Type = Types.SportsEquipment, Description = "Polarized sunglasses", Price = 59.99 },
                new Product { Name = "Waterproof Jacket", Type = Types.Clothing, Description = "Lightweight waterproof jacket", Price = 89.99 },
                new Product { Name = "Running Shoes", Type = Types.Clothing, Description = "Comfortable running shoes", Price = 129.99 }
            );

            context.SaveChanges();
        }
    }
}
