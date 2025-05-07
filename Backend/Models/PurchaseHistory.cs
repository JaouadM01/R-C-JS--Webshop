
namespace Backend.Models{
    public class PurchaseHistory {
        public Guid Id {get; set;}
        public Guid PreviousOwner {get; set;}
        public Guid NewOwner {get; set;}
        public Guid ProductId {get; set;}
        public DateTime ChangedDate {get; set;}
    }
}