namespace Backend.Dtos {
    using Backend.Models;

    public class ProductDto {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public string? Image{get; set;}
        public Status Status {get; set;}
    }
}