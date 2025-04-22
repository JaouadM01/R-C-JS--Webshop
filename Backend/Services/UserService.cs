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
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository repo, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _repo = repo;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
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
            existingUser.Role = user.Role;

            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password = _passwordHasher.HashPassword(existingUser, user.Password);
            }

            await _repo.Update(existingUser);
            return _mapper.Map<UserDto>(existingUser);
        }

    }
}