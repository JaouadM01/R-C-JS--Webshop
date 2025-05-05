using Backend.Models;
using Backend.Dtos;
using AutoMapper;
using Backend.Repositories;

namespace Backend.Services
{

    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<Product> Create(ProductDto productDto, Guid userId);
        Task<ProductDto> Update(Guid id, ProductDto product);
        Task<bool> Delete(Guid id);
        Task<ProductDto?> GetById(Guid id);
        Task<IEnumerable<ProductDto>> GetProductListById(Guid id);
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
            var productDtos = products.Select(product =>
        {
            var productDto = _mapper.Map<ProductDto>(product);
            productDto.Type = product.Type.ToString(); // Convert enum to string
            return productDto;
        });
            return productDtos;
        }

        public async Task<Product> Create(ProductDto productDto, Guid userId)
        {
            var product = _mapper.Map<Product>(productDto);
            product.UserId = userId;
            await _repo.Create(product);
            return product;
        }
        public async Task<ProductDto> Update(Guid id, ProductDto product)
        {
            var existingProduct = await _repo.GetByIdAsync(id);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {product.Id}:{id} not found.");

            // Update the existing product
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Type = Enum.Parse<Backend.Models.Types>(product.Type, true);
            existingProduct.Price = product.Price;
            existingProduct.Image = product.Image;

            await _repo.Update(existingProduct);

            return _mapper.Map<ProductDto>(existingProduct);
        }

        public async Task<bool> Delete(Guid id)
        {
            var existingProduct = await _repo.GetByIdAsync(id);
            if (existingProduct == null) return false;

            await _repo.Delete(existingProduct);
            return true;
        }

        public async Task<ProductDto?> GetById(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return null;

            var productDto = _mapper.Map<ProductDto>(product);

            // Convert enum to string
            productDto.Type = product.Type.ToString(); // This automatically converts it to a string

            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductListById(Guid id)
        {
            var products = await _repo.GetProductListById(id);
            if (products == null) return Enumerable.Empty<ProductDto>();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

    }
}