namespace Backend.Dtos
{
    public class ReceiptProductDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}