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

            // Create NFT products linked to the ProductSeller
            var products = new List<Product>
            {
                new Product
                {
                    Name = "#001",
                    Type = Types.Electronics,
                    Description = "Head Type: Bitcoin\nCategory: Rare\nBackground Color: Red\nSuit Color: Black\nAccessory: None\nDescription: Classic Bitcoin head with no accessories, dressed in a sharp black suit. The red background gives off a bold, energetic vibe.",
                    Price = 1000000.00M,
                    UserId = productSeller.Id,
                    Image = "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafybeibm4jj5hk3canec3emrg2xjw5zgjfafcmozj3az2q5csf73ik3s6e",
                    Status = Status.Listed,
                    Rarity = Rarity.Rare
                },
                new Product
                {
                    Name = "#002",
                    Type = Types.Electronics,
                    Description = "Head Type: Bitcoin\nCategory: Rare\nBackground Color: Blue\nSuit Color: Navy Blue\nAccessory: None\nDescription: Classic Bitcoin head with no accessories, dressed in a sharp navy blue suit. The blue background adds a clean and dependable look.",
                    Price = 1000000.00M,
                    UserId = productSeller.Id,
                    Image = "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafybeie67vgsd375aksuph6yfesgsi5e4tujrylhp2zrw7c2synqmypqsi",
                    Status = Status.Listed,
                    Rarity = Rarity.Rare
                },
                new Product
                {
                    Name = "#003",
                    Type = Types.Electronics,
                    Description = "Head Type: Solana\nCategory: Rare\nBackground Color: Red\nSuit Color: Green\nAccessory: Neon Glasses\nDescription: The Solana head with funky neon glasses is paired with a green suit and red background for a cool and retro vibe.",
                    Price = 1000000.00M,
                    UserId = productSeller.Id,
                    Image = "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafybeichk4uha4tdf2qr5reawyub4cw4rh6lt7y6bvczcuaxfaarehwhme",
                    Status = Status.Listed,
                    Rarity = Rarity.Rare
                },
                new Product
                {
                    Name = "#004",
                    Type = Types.Electronics,
                    Description = "Head Type: Ethereum\nCategory: Rare\nBackground Color: Orange\nSuit Color: Green\nAccessory: None\nDescription: Ethereum head with a green suit and orange background, combining modern colors for a balanced and lively look.",
                    Price = 1000000.00M,
                    UserId = productSeller.Id,
                    Image = "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafybeia7bvxolv7zufw3i4bytdotdpe7j62ecaqvkq4swb4imelpf4tftu",
                    Status = Status.Listed,
                    Rarity = Rarity.Rare
                },
                new Product
                {
                    Name = "#005",
                    Type = Types.Electronics,
                    Description = "Head Type: Ethereum\nCategory: Rare\nBackground Color: Grey\nSuit Color: White\nAccessory: None\nDescription: Classic Ethereum head in a white suit with a grey background adding elegance and class.",
                    Price = 1000000.00M,
                    UserId = productSeller.Id,
                    Image = "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafybeidimhb537vlqcymy2os6jy6oakmni35oeo4oruwrd35p53dnbotre",
                    Status = Status.Listed,
                    Rarity = Rarity.Rare
                },
                new Product
                {
                    Name = "#006",
                    Type = Types.Electronics,
                    Description = "Head Type: Phone\nCategory: UnCommon\nBackground Color: Orange\nSuit Color: Purple\nAccessory: Skull with Sunglasses\nDescription: A unique phone with a skull and sunglasses as the head, dressed in a sharp purple suit. The orange background creates a fun and vibrant look.",
                    Price = 100000.00M,
                    UserId = productSeller.Id,
                    Image = "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafybeib3phxgw7o3gnaqtl2a6lc56asum6nznju7xi4jdfy6y53yx75kme",
                    Status = Status.Listed,
                    Rarity = Rarity.Uncommon
                },
                new Product
                {
                    Name = "#007",
                    Type = Types.Electronics,
                    Description = "Head Type: Laptop\nCategory: UnCommon\nBackground Color: Blue\nSuit Color: Green\nAccessory: None\nDescription: A sleek laptop head with a minimalist design, paired with a stylish green suit. The blue background adds a modern and technological touch.",
                    Price = 100000.00M,
                    UserId = productSeller.Id,
                    Image = "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafybeibomfxukplrwwr4nelt2t2waox7kl2e53jiac7b7pudinuevkfxve",
                    Status = Status.Listed,
                    Rarity = Rarity.Uncommon
                },
                new Product
                {
                    Name = "#008",
                    Type = Types.Electronics,
                    Description = "Head Type: Noodle Bowl\nCategory: Common\nBackground Color: Orange\nSuit Color: Orange\nAccessory: Neon Sunglasses\nDescription: A rare noodle bowl head with a pair of neon sunglasses, paired with a sharp orange suit. The vibrant orange background creates a lively and bold look.",
                    Price = 10000.00M,
                    UserId = productSeller.Id,
                    Image = "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafybeiayfxr2iibmizvuxcbplgrtbwbzj6hh5aezrus466nunq2ozfjiqq",
                    Status = Status.Listed,
                    Rarity = Rarity.Common
                },
                new Product
                {
                    Name = "#009",
                    Type = Types.Electronics,
                    Description = "Head Type: Donut\nCategory: Common\nBackground Color: Purple\nSuit Color: Navy Blue\nAccessory: USB Necklace\nDescription: A rare donut head with colorful sprinkles, dressed in a classy navy blue suit. The purple background adds a playful and modern touch to the overall look.",
                    Price = 10000.00M,
                    UserId = productSeller.Id,
                    Image = "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafkreicwuwv232y7oph6w6iqwolsm3iib56rescxanspbu2qro3342gfii",
                    Status = Status.Listed,
                    Rarity = Rarity.Common
                },
                new Product
{
    Name = "#015",
    Type = Types.Electronics,
    Description = "Head Type: TV Screen\nCategory: Rare\nBackground Color: Black\nSuit Color: Charcoal Grey\nAccessory: USB Necklace\nDescription: A stylish figure with a TV screen for a head in a charcoal grey suit with a USB necklace.",
    Price = 100000.00M,
    UserId = productSeller.Id,
    Image = "https://gateway.pinata.cloud/ipfs/bafkreiewtdy3c6ah7zm3kin5nw6c4d2durm7f23ep3tpnorfd4o4yqslee",
    Status = Status.Listed,
    Rarity = Rarity.Uncommon
},
new Product
{
    Name = "#018",
    Type = Types.Electronics,
    Description = "Head Type: TV Screen\nCategory: Rare\nBackground Color: Yellow\nSuit Color: Ivory White\nAccessory: USB Necklace\nDescription: A stylish figure with a TV screen for a head, wearing an ivory white suit and a USB necklace.",
    Price = 100000.00M,
    UserId = productSeller.Id,
    Image = "https://gateway.pinata.cloud/ipfs/bafybeifnuw7shk67ugrli5mlrlkcsrp5qmtd6exfws7r5gaa5drxhebjwy",
    Status = Status.Listed,
    Rarity = Rarity.Uncommon
},
new Product
{
    Name = "#063",
    Type = Types.Electronics,
    Description = "Head Type: Ice Cream\nCategory: Rare\nBackground Color: Teal\nSuit Color: Royal Purple\nAccessory: Neon Pink Glasses\nDescription: A stylish figure with an ice cream for a head, wearing a royal purple suit and neon pink sunglasses.",
    Price = 100000.00M,
    UserId = productSeller.Id,
    Image = "https://gateway.pinata.cloud/ipfs/bafkreigfwbxpxdanxzifhqe2sqco3gtknp6ddsmq3ly35l6pqm3no4qtci",
    Status = Status.Listed,
    Rarity = Rarity.Common
},
new Product
{
    Name = "#065",
    Type = Types.Electronics,
    Description = "Head Type: Donut\nCategory: Common\nBackground Color: Orange\nSuit Color: Emerald Green\nAccessory: None\nDescription: A stylish figure with a donut for a head, wearing an emerald green suit.",
    Price = 100000.00M,
    UserId = productSeller.Id,
    Image = "https://gateway.pinata.cloud/ipfs/bafkreibwxolggi2qgavpok73oujfuzvlpodubaumwm6npvnegtqqrzfgge",
    Status = Status.Listed,
    Rarity = Rarity.Common
},
new Product
{
    Name = "#067",
    Type = Types.Electronics,
    Description = "Head Type: Fries Hair\nCategory: Rare\nBackground Color: Beige\nSuit Color: Royal Purple\nAccessory: Neon Glasses\nDescription: A stylish figure with a French fries container for a head, wearing a royal purple suit and neon glasses.",
    Price = 100000.00M,
    UserId = productSeller.Id,
    Image = "https://gateway.pinata.cloud/ipfs/bafkreifr7fzp3kh4ygp6pnjaevtkztmmvwgqysyqcir54lklve3gwzf6be",
    Status = Status.Listed,
    Rarity = Rarity.Common
},
new Product
{
    Name = "#068",
    Type = Types.Electronics,
    Description = "Head Type: Coffee Cup\nCategory: Rare\nBackground Color: Beige\nSuit Color: Black\nAccessory: AirPods\nDescription: A stylish figure with a coffee cup for a head, wearing a black suit and AirPods.",
    Price = 100000.00M,
    UserId = productSeller.Id,
    Image = "https://gateway.pinata.cloud/ipfs/bafybeibfecz2nee7pmhlc4ppmx3ptnjpeev6daedviwc5pyrcarvwqm7f4",
    Status = Status.Listed,
    Rarity = Rarity.Common
},
new Product
{
    Name = "#069",
    Type = Types.Electronics,
    Description = "Head Type: Pizza Slice with Pepperoni, Olives, and Green Peppers\nCategory: Rare\nBackground Color: Beige\nSuit Color: Black\nAccessory: None\nDescription: A figure with a pizza slice head, topped with pepperoni, olives, and green peppers, wearing a black suit with a beige background.",
    Price = 100000.00M,
    UserId = productSeller.Id,
    Image = "https://gateway.pinata.cloud/ipfs/bafybeidwe6jmgpda3wrhia6tjtufksu4pqln6s5oov2ydscnqe3cwbpgni",
    Status = Status.Listed,
    Rarity = Rarity.Common
},
new Product
{
    Name = "#070",
    Type = Types.Electronics,
    Description = "Head Type: Donut with Glasses\nCategory: Common\nBackground Color: Beige\nSuit Color: Black\nAccessory: Glasses\nDescription: A figure with a red donut head, wearing glasses, and a black suit against a beige background.",
    Price = 100000.00M,
    UserId = productSeller.Id,
    Image = "https://gateway.pinata.cloud/ipfs/bafybeidmhtx4t4tf4coykdqvzhefv7plxemi5f2sj4eylilzpsu2eod5cm",
    Status = Status.Listed,
    Rarity = Rarity.Common
}


            };

            // Add users and products to the context
            context.Users.AddRange(backendEmployee, productSeller, customer);
            context.Products.AddRange(products);

            context.SaveChanges();
        }
    }
}
