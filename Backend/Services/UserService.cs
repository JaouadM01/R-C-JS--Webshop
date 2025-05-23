using Backend.Models;
using Backend.Dtos;
using AutoMapper;
using Backend.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{

    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> Create(UserDto userDto);
        Task<UserDto?> Update(Guid id, UserDto user);
        Task<string?> Login(string email, string password);
        Task<bool> Delete(Guid id);
        Task<UserDto?> GetById(Guid id);
        Task<UserDto?> GetUserProfileAsync(Guid userId);
        Task<List<Guid>?> GetFavourites(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IProductService _productservice;

        public UserService(IUserRepository repo, IMapper mapper, IPasswordHasher<User> passwordHasher, ITokenService tokenService, IProductService productService)
        {
            _repo = repo;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _productservice = productService;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            var userDtos = users.Select(user =>
    {
        var userDto = _mapper.Map<UserDto>(user);
        userDto.Role = user.Role.ToString(); // Convert enum to string
        return userDto;
    });

            return userDtos;
        }

        public async Task<UserDto?> Create(UserDto userDto)
        {
            // Map the incoming user DTO to a User entity
            var user = _mapper.Map<User>(userDto);

            // Hash the user's password before saving it to the database
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.Password = _passwordHasher.HashPassword(user, userDto.Password);
            }

            // Save the user in the database
            var created = await _repo.Create(user);

            // Return the created user as a DTO
            return created != null ? _mapper.Map<UserDto>(created) : null;
        }


        public async Task<UserDto?> Update(Guid id, UserDto user)
        {
            var existingUser = await _repo.GetByIdAsync(id);
            if (existingUser == null) return null;

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Role = Enum.TryParse<UserRole>(user.Role, out var parsedRole) ? parsedRole : existingUser.Role;
            // possible bad update -> from role to string in dto

            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password = _passwordHasher.HashPassword(existingUser, user.Password);
            }

            await _repo.Update(existingUser);
            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task<string?> Login(string email, string password)
        {
            var existingUser = await _repo.GetByEmailAsync(email);
            if (existingUser == null) return null;

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, password);
            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return _tokenService.GenerateToken(existingUser);
            }

            return null;
        }
        public async Task<bool> Delete(Guid id)
        {
            var existingUser = await _repo.GetByIdAsync(id);
            if (existingUser == null) return false;

            await _repo.Delete(existingUser);
            return true;
        }

        public async Task<UserDto?> GetById(Guid id)
        {
            var existingUser = await _repo.GetByIdAsync(id);
            if (existingUser == null) return null;

            var userDto = _mapper.Map<UserDto>(existingUser);

            userDto.Role = existingUser.Role.ToString();

            return userDto;
        }

        public async Task<bool> Favourite(Guid productId, Guid userId)
        {
            // Get the existing user from the repository
            var existingUser = await _repo.GetByIdAsync(userId);

            // If the user doesn't exist, return false
            if (existingUser == null) return false;

            // If the user doesn't have any favourites yet, initialize an empty list
            if (existingUser.Favourites == null)
            {
                existingUser.Favourites = new List<Guid>();
            }

            // Check if the product is already in the user's favourites
            if (existingUser.Favourites.Contains(productId))
            {
                return false;  // The product is already in the favourites list
            }

            // Add the product to the user's favourites list
            existingUser.Favourites.Add(productId);

            // Update the user in the repository
            await _repo.Update(existingUser);

            return true;  // Return true to indicate that the product was successfully added
        }

        public async Task<bool> UnFavourite(Guid productId, Guid userId)
        {
            // Get the existing user
            var existingUser = await _repo.GetByIdAsync(userId);
            if (existingUser == null) return false;

            // If the user doesn't have any favourites yet, return false
            if (existingUser.Favourites == null || !existingUser.Favourites.Contains(productId))
            {
                return false;  // Product not in favourites
            }

            // Remove the product from the favourites list
            existingUser.Favourites.Remove(productId);

            // Update the user in the repository
            await _repo.Update(existingUser);

            return true;  // Successfully removed from favourites
        }

        public async Task<UserDto?> GetUserProfileAsync(Guid userId)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user == null)
            {
                return null; // User not found
            }

            var userDto = _mapper.Map<UserDto>(user);

            userDto.Role = user.Role.ToString();

            return userDto;

        }

        public async Task<List<Guid>?> GetFavourites(Guid id)
        {
            var favouriteslist = await _repo.GetFavourites(id);
            if (favouriteslist == null) return null;
            return favouriteslist;
        }

    }
}