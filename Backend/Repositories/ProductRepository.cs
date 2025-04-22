using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task Create(Product product);
        Task Update(Product product);
        Task<Product?> GetByIdAsync(Guid id);
        Task Delete(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating product: {ex}");
                throw; // Still throw so the service/controller can handle it
            }
        }

        public async Task Update(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating product: {ex}");
                throw; // Still throw so the service/controller can handle it 
            }
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Products.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating product: {ex}");
                throw; // Still throw so the service/controller can handle it 
            }
        }

        public async Task Delete(Product product){
            try {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex){
                Console.WriteLine($"Error occured while deleting product: {ex}");
                throw;
            }
        }
    }
}
