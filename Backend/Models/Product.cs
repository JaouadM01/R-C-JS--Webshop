namespace Backend.Models
{
    public class Product {
        public int Id{get; set;}
        public required string Name{get; set;}
        public required Types Type{get; set;}
        public required string Description{get; set;}
        public required double Price{get; set;}
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
}