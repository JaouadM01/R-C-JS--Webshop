using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public interface IReceiptRepository
    {
        Task CreateReceipt(Receipt receipt);
        Task CreateReceiptProduct(List<ReceiptProduct> receiptProduct);
        Task<IEnumerable<Receipt>> GetAllAsync();
        Task Delete(Guid id);
        Task<Receipt?> GetByIdAsync(Guid id);
        Task Update(Receipt receipt);
        Task RemoveRP(Guid id);
        Task Remove(Guid id);
    }

    public class ReceiptRepository : IReceiptRepository
    {
        private readonly AppDbContext _context;

        public ReceiptRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateReceipt(Receipt receipt)
        {
            try
            {
                _context.Receipts.Add(receipt);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task CreateReceiptProduct(List<ReceiptProduct> receiptProducts)
        {
            try
            {
                _context.ReceiptProducts.AddRange(receiptProducts);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        
        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            try {
                return await _context.Receipts.Include(r => r.ReceiptProducts).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            try {
                var receipt = await _context.Receipts.FirstOrDefaultAsync(r => r.Id == id);
                if (receipt != null)
                {
                    _context.Receipts.Remove(receipt);
                    await _context.SaveChangesAsync();
                }
            }
            catch{
                throw;
            }
        }

        public async Task<Receipt?> GetByIdAsync(Guid id)
        {
            try {
                var receipt = await _context.Receipts.FirstOrDefaultAsync(r => r.Id == id);
                if (receipt != null)
                {
                    return receipt;
                }
            }
            catch{
                throw;
            }
            return null;
        }
        public async Task Update(Receipt receipt)
        {
            try {
                _context.Receipts.Update(receipt);
                await _context.SaveChangesAsync();
            }
            catch{
                throw;
            }
        }

        public async Task RemoveRP(Guid id)
        {
            try {
                var rp = await _context.ReceiptProducts.FirstOrDefaultAsync(r => r.ProductId == id);
                if (rp != null)
                {
                    _context.ReceiptProducts.Remove(rp);
                    await _context.SaveChangesAsync();
                }
            }
            catch {
                throw;
            }
        }
        public async Task Remove(Guid id)
        {
            try {
                var rp = await _context.ReceiptProducts.FirstOrDefaultAsync(r => r.Id == id);
                if (rp != null)
                {
                    _context.ReceiptProducts.Remove(rp);
                    await _context.SaveChangesAsync();
                }
            }
            catch {
                throw;
            }
        }
    }
}