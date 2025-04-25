namespace Backend.Models
{
    public class ReceiptProduct
    {
        public Guid Id { get; set; }
        public Guid ReceiptId { get; set; }  // Foreign key to Receipt
        public Receipt Receipt { get; set; }
        
        public Guid ProductId { get; set; }  // Foreign key to Product
        public Product Product { get; set; }
        
        public int Quantity { get; set; }  // Quantity purchased
        public decimal Price { get; set; }  // Price at the time of purchase
    }
}
