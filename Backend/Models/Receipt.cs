namespace Backend.Models
{
    public class Receipt
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }  // User who made the purchase
        public DateTime CreatedAt { get; set; }  // Time of purchase
        public decimal TotalAmount { get; set; }  // Total amount of the receipt
        public List<ReceiptProduct> ReceiptProducts { get; set; } = new List<ReceiptProduct>();  // List of products in the receipt
    }
}
