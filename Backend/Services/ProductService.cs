using Backend.Models;
using Backend.Dtos;
using AutoMapper;
using Backend.Repositories;

namespace Backend.Services
{

    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<Product> Create (ProductDto productDto, Guid userId);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<Product> Create(ProductDto productDto, Guid userId)
        {
            var product = _mapper.Map<Product>(productDto);
            product.UserId = userId;
            await _repo.Create(product);
            return product;
        }
    }
}