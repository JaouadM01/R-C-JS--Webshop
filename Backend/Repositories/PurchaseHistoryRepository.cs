using Backend.Data;
using Backend.Models;

namespace Backend.Repositories
{
    public interface IPurchaseHistoryRepository
    {
        Task Create(Guid previousOwner, Guid newOwner, Guid productId);
    }

    public class PurchaseHistoryRepository : IPurchaseHistoryRepository
    {
        private readonly AppDbContext _context;

        public PurchaseHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Guid previousOwner, Guid newOwner, Guid productId)
        {
            try {
                var newRecord = new PurchaseHistory {
                    Id = Guid.NewGuid(),
                    PreviousOwner = previousOwner,
                    NewOwner = newOwner,
                    ProductId = productId,
                    ChangedDate = DateTime.UtcNow
                };

                _context.PurchaseHistory.Add(newRecord);
                await _context.SaveChangesAsync();
            }
            catch{
                throw;
            }
        }

    }
}