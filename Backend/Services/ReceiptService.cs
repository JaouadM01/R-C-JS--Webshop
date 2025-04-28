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

            List<Guid> listofrpneededtobedeleted = new List<Guid>();

            existingReceipt.Id = receiptDto.Id;
            existingReceipt.UserId = receiptDto.UserId;
            existingReceipt.TotalAmount = receiptDto.TotalAmount;

            // stap 1: verwijder oudere producten die niet meer in de lijst staan
            var productIdsExisting = existingReceipt.ReceiptProducts.Select(r => r.Id).ToList();
            var newReceiptProducts = _mapper.Map<List<ReceiptProduct>>(receiptDto.ReceiptProducts);
            var productsToRemove = existingReceipt.ReceiptProducts.Where(rp => !newReceiptProducts.Any(nrp => nrp.Id == rp.Id)).ToList();
            foreach (var rp in productsToRemove)
            {
                await _receiptRepository.RemoveRP(rp.ProductId);
            }

            // stap 2: toevoegen van nieuwe producten
            foreach (var newProduct in newReceiptProducts)
            {
                var ProductNeededUpdate = existingReceipt.ReceiptProducts.FirstOrDefault(rp => rp.Id == newProduct.Id);
                if(ProductNeededUpdate != null)
                {
                    ProductNeededUpdate.Quantity = newProduct.Quantity;
                    ProductNeededUpdate.Price = newProduct.Price;
                    listofrpneededtobedeleted.Add(ProductNeededUpdate.Id);
                }
                else
                {
                    newProduct.ReceiptId = existingReceipt.Id;
                    existingReceipt.ReceiptProducts.Add(newProduct);
                }
            }
            // stap 3: updaten en terug sturen van een dto

            // delete de bijgewerkte ReceiptProduct
            foreach(Guid receiptProductId in listofrpneededtobedeleted){
                await _receiptRepository.Remove(receiptProductId);
            }

            await _receiptRepository.Update(existingReceipt);

            return _mapper.Map<ReceiptDto>(existingReceipt);
        }
    }
}
