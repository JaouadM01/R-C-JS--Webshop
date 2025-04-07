using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
    }

    public class ProductRepository(AppDbContext context) : IProductRepository {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Product>> GetAllAsync(){
            return await _context.Products.ToListAsync();
        }
    }
}
