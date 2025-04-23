namespace Backend.Dtos {

    public class PurchaseDto {
        public required Guid Id{get; set;}
        public required Guid ProductId{get; set;}
        public required int Amount{get; set;}
    }
}