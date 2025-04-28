using Backend.Models;
using Backend.Dtos;
using AutoMapper;
using Backend.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Backend.Services
{
    public interface IReceiptService
    {
        Task<ReceiptDto?> CreateReceipt(Guid UserId, List<ReceiptProductRequest> products);
        Task<IEnumerable<ReceiptDto?>> GetAllAsync();
        Task Delete(Guid id);
        Task<ReceiptDto?> Update(Guid id, ReceiptDto receiptDto);
    }

    public class ReceiptService : IReceiptService
    {
        private readonly IProductRepository _productRepository;
        private readonly IReceiptRepository _receiptRepository;
        private readonly IMapper _mapper;

        public ReceiptService(IProductRepository productRepository, IReceiptRepository receiptRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _receiptRepository = receiptRepository;
            _mapper = mapper;
        }

        public async Task<ReceiptDto?> CreateReceipt(Guid UserId, List<ReceiptProductRequest> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products), "Products list cannot be null.");

            var Receipt = new Receipt
            {
                Id = Guid.NewGuid(),
                UserId = UserId,
                CreatedAt = DateTime.UtcNow,
                TotalAmount = 0
            };

            var receiptProducts = new List<ReceiptProduct>();
            foreach (var product in products)
            {
                var existingProduct = await _productRepository.GetByIdAsync(product.ProductId);
                if (existingProduct == null) continue;

                var amount = product.Quantity * existingProduct.Price;
                Receipt.TotalAmount += amount;

                receiptProducts.Add(new ReceiptProduct
                {
                    Id = Guid.NewGuid(),
                    ReceiptId = Receipt.Id,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    Price = existingProduct.Price
                });
            }

            if (receiptProducts.Any())
            {
                await _receiptRepository.CreateReceipt(Receipt);
                await _receiptRepository.CreateReceiptProduct(receiptProducts);

                return _mapper.Map<ReceiptDto>(Receipt);
            }

            return null;
        }
        public async Task<IEnumerable<ReceiptDto?>> GetAllAsync()
        {
            var receipts = await _receiptRepository.GetAllAsync();
            if (receipts == null) return Enumerable.Empty<ReceiptDto>();
            return _mapper.Map<IEnumerable<ReceiptDto>>(receipts);
        }

        public async Task Delete(Guid id)
        {
            var receipt = await _receiptRepository.GetByIdAsync(id);
            if (receipt == null)
            {
                throw new InvalidOperationException($"Receipt with ID {id} not found.");
            }

            await _receiptRepository.Delete(id);
        }

        public async Task<ReceiptDto?> Update(Guid id, ReceiptDto receiptDto)
        {
            var existingReceipt = await _receiptRepository.GetByIdAsync(id);
            if (existingReceipt == null) return null;

            // change userid if it is changed in the new receipt
            if (existingReceipt.UserId != receiptDto.UserId) existingReceipt.UserId = receiptDto.UserId;

            // Ensure ReceiptProducts are not null
            if (existingReceipt.ReceiptProducts != null && receiptDto.ReceiptProducts != null)
            {
                // Identify products to remove
                var productsNeededToRemove = existingReceipt.ReceiptProducts
                    .Where(rp => !receiptDto.ReceiptProducts.Any(rpDto => rpDto.ProductId == rp.ProductId))
                    .ToList(); // Use ToList to avoid modifying the collection during iteration

                // Remove the identified products
                foreach (var product in productsNeededToRemove)
                {
                    existingReceipt.ReceiptProducts.Remove(product);
                }
            }

            // Add or update products
            foreach(var updatedProduct in receiptDto.ReceiptProducts)
            {
                var existingProduct = existingReceipt.ReceiptProducts?.FirstOrDefault(rp => rp.Id == updatedProduct.Id);
                
                if(existingProduct != null)
                {
                    existingProduct.Quantity = updatedProduct.Quantity;
                    existingProduct.Price = updatedProduct.Price;
                }
                else
                {
                    var newProduct = new ReceiptProduct {
                        Id = Guid.NewGuid(),
                        ProductId = updatedProduct.ProductId,
                        Quantity = updatedProduct.Quantity,
                        Price = updatedProduct.Price,
                        ReceiptId = id
                    };
                    existingReceipt.ReceiptProducts.Add(newProduct);
                }

            }

            // calculating total amount
            existingReceipt.TotalAmount = existingReceipt.ReceiptProducts.Sum(rp => rp.Quantity * rp.Price);

            await _receiptRepository.Update(existingReceipt);

            return _mapper.Map<ReceiptDto>(existingReceipt);
        }
    }
}
