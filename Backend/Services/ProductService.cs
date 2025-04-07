using Backend.Models;
using Backend.Dtos;
using AutoMapper;
using Backend.Repositories;

namespace Backend.Services {

    public interface IProductService {
        Task<IEnumerable<ProductDto>> GetAllAsync();
    }

    public class ProductService : IProductService{
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper){
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync() {
            var products = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}