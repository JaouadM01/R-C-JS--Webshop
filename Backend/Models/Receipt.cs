namespace Backend.Models {
    public class Receipt {
        public required Guid Id{get; set;}
        public required Guid UserId{get; set;}
        public required List<Guid> Purchases{get; set;}
        public decimal TotalAmount{get; set;}
    }
}