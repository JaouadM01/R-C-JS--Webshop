using Backend.Models;
using Backend.Dtos;
using AutoMapper;
using Backend.Repositories;

namespace Backend.Services {

    public interface IUserService {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> Create(UserDto userDto);
    }

    public class UserService : IUserService{
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper){
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync() {
            var users = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> Create(UserDto userDto){
            var user = _mapper.Map<User>(userDto);
            var created = await _repo.Create(user);
            return created != null ? _mapper.Map<UserDto>(created) : null;
        }

        
    }
}