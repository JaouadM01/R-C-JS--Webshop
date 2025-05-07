using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public interface IPurchaseHistoryRepository
    {
        Task Create(Guid previousOwner, Guid newOwner, Guid productId);
        Task<List<PurchaseHistory>> GetAllAsync();
        Task<List<PurchaseHistory>> GetByIdAsync(Guid id);
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

        public async Task<List<PurchaseHistory>> GetAllAsync()
        {
            try {
                return await _context.PurchaseHistory.ToListAsync();
            }
            catch {
                throw;
            }
        }
        public async Task<List<PurchaseHistory>> GetByIdAsync(Guid id)
        {
            try {
                return await _context.PurchaseHistory.Where(p => p.NewOwner == id || p.PreviousOwner == id).ToListAsync();
            }
            catch {
                throw;
            }
        }

    }
}