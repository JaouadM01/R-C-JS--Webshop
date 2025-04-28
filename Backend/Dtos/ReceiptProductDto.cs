namespace Backend.Dtos
{
    public class ReceiptProductDto
    {
        public Guid Id { get; set; }       // The unique identifier for ReceiptProduct (this should match ReceiptProduct.Id)
        public Guid ProductId { get; set; } // Foreign key to Product
        public Guid ReceiptId { get; set; } // Foreign key to Receipt
        public int Quantity { get; set; }   // Quantity purchased
        public decimal Price { get; set; }  // Price at the time of purchase
    }
}
