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
        Task<ProductDto> UpdateOwner(Guid id, Guid productId);
        Task<IEnumerable<ProductDto>> GetAllListedAsync();
        Task<ProductDto> ListProduct(Guid id, Guid userId);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPurchaseHistoryRepository _repoHistory;

        public ProductService(IProductRepository repo, IMapper mapper, IPurchaseHistoryRepository repoHistory)
        {
            _repo = repo;
            _mapper = mapper;
            _repoHistory = repoHistory;
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
        public async Task<IEnumerable<ProductDto>> GetAllListedAsync()
        {
            var products = await _repo.GetAllListedAsync();
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
            // Validate and map enum
            if (!Enum.TryParse<Types>(productDto.Type, ignoreCase: true, out var parsedType))
                throw new ArgumentException($"Invalid product type: {productDto.Type}");

            // Construct Product manually
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Type = parsedType,
                Description = productDto.Description,
                Price = productDto.Price,
                Image = string.IsNullOrWhiteSpace(productDto.Image)
                ? "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafkreiaplp3byr2xhkempkdhpjqxuedl5ez5anygqs6lfzoaqcdgg5rauu"
                : productDto.Image,
                Status = productDto.Status,
                UserId = userId
            };

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

        public async Task<ProductDto> UpdateOwner(Guid id, Guid productId)
        {
            var existingProduct = await _repo.GetByIdAsync(productId);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            var previousOwner = existingProduct.UserId;

            // Only update if the new owner is different
            if (existingProduct.UserId != id)
            {
                existingProduct.UserId = id;
                existingProduct.Status = Status.Owned;
                await _repo.Update(existingProduct);

                // Create record of sale
                try
                {
                    await _repoHistory.Create(previousOwner, existingProduct.UserId, productId);
                }
                catch (Exception ex)
                {
                    // Optionally, log the error here
                    throw new InvalidOperationException("Error recording purchase history.", ex);
                }

                return _mapper.Map<ProductDto>(existingProduct);
            }
            else
            {
                throw new InvalidOperationException("The product already belongs to the specified user.");
            }
        }

        public async Task<ProductDto> ListProduct(Guid id, Guid userId)
        {
            // Retrieve the product by ID
            var existingProduct = await _repo.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return null;  // Return null if the product doesn't exist
            }

            // Ensure the user is the owner of the product
            if (existingProduct.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to modify this product.");
            }

            // Toggle the product's status between "Owned" and "Listed"
            if (existingProduct.Status == Status.Owned)
            {
                existingProduct.Status = Status.Listed;  // Change status to Listed
            }
            else if (existingProduct.Status == Status.Listed)
            {
                existingProduct.Status = Status.Owned;  // Change status back to Owned
            }

            // Update the product status in the repository
            await _repo.Update(existingProduct);

            return _mapper.Map<ProductDto>(existingProduct);  // Return success message
        }



    }
}