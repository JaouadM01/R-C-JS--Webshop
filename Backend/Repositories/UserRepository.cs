using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> Create(User user);
        Task Update(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task Delete(User user);
        Task<User?> GetById(Guid id);
        Task<List<Guid>> GetFavourites(Guid id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> Create(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch
            {
                // Optional: log or handle specific exceptions
                return null;
            }
        }

        public async Task Update(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // Optional: log or handle specific exceptions
                throw;
            }
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task Delete(User user)
        {
            try {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // Optional: log the exception
                throw;
            }
        }

        public async Task<User?> GetById(Guid id)
        {
            try {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if(existingUser == null) return null;
                return existingUser;
            }
            catch{
                throw;
            }
        }

        public async Task<List<Guid>> GetFavourites(Guid id)
        {
            try {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if(existingUser == null || existingUser.Favourites == null) return new List<Guid>();
                return existingUser.Favourites;
            }
            catch {
                throw;
            }
        }
    }




}
