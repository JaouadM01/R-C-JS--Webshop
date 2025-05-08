namespace Backend.Models
{
    public class Product {
        public Guid Id{get; set;}
        public required string Name{get; set;}
        public required Types Type{get; set;}
        public required string Description{get; set;}
        public required decimal Price{get; set;}

        // ensure link between product and user
        public Guid UserId{get; set;}
        public User? User{get; set;}
        // link to the image on IPFS Pinata
        public string? Image{get; set;}
        public Status Status {get; set;}
    }
    public enum Types {
        Electronics,
        Clothing,
        HomeAppliances,
        Books,
        Toys,
        Groceries,
        Furniture,
        SportsEquipment,
        BeautyProducts,
        Automotive
    }

    public enum Status {
        Owned,
        Listed
    }
}