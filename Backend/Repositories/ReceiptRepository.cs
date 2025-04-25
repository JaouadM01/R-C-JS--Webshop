using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public interface IReceiptRepository
    {
        Task CreateReceipt(Receipt receipt);
        Task CreateReceiptProduct(List<ReceiptProduct> receiptProduct);
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

    }
}