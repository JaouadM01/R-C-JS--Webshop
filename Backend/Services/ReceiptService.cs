using Backend.Models;
using Backend.Dtos;
using AutoMapper;
using Backend.Repositories;


namespace Backend.Services
{
    public interface IReceiptService
    {
        Task<ReceiptDto> CreateReceipt(Guid UserId , List<ReceiptProductRequest> products);
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

        public async Task<ReceiptDto> CreateReceipt(Guid UserId , List<ReceiptProductRequest> products)
        {
            if(products == null) return null;

            var Receipt = new Receipt {
                Id = Guid.NewGuid(),
                UserId = UserId,
                CreatedAt = DateTime.UtcNow,
                TotalAmount = 0
            };

            var receiptProducts = new List<ReceiptProduct>();
            foreach(var product in products)
            {
                var existingProduct = await _productRepository.GetByIdAsync(product.ProductId);
                if(existingProduct == null) continue;

                var amount = product.Quantity * existingProduct.Price;
                Receipt.TotalAmount += amount;

                receiptProducts.Add( new ReceiptProduct {
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
    }
}
