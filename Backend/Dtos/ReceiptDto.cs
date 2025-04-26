namespace Backend.Dtos
{
    public class ReceiptDto
    {
        public Guid Id { get; set; }  // The unique identifier for the receipt
        public Guid UserId { get; set; }  // The user who made the purchase
        public DateTime CreatedAt { get; set; }  // The date and time of purchase
        public decimal TotalAmount { get; set; }  // The total amount of the receipt
        public List<ReceiptProductDto> ReceiptProducts { get; set; } = new List<ReceiptProductDto>();  // List of products in the receipt
    }
}
