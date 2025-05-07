using Backend.Models;
using Backend.Dtos;
using AutoMapper;
using Backend.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{

    public interface IPurchaseHistoryService
    {
        Task<IEnumerable<PurchaseHistory>>? GetByIdAsync(Guid id);
        Task<IEnumerable<PurchaseHistory>>? GetAllAsync();
    }

    public class PurchaseHistoryService : IPurchaseHistoryService
    {
        private readonly IPurchaseHistoryRepository _repo;
        private readonly IMapper _mapper;

        public PurchaseHistoryService(IPurchaseHistoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseHistory>> GetAllAsync()
        {
            var history = await _repo.GetAllAsync();
            return history ?? Enumerable.Empty<PurchaseHistory>();
        }
        public async Task<IEnumerable<PurchaseHistory>> GetByIdAsync(Guid id)
        {
            var historyList = await _repo.GetByIdAsync(id);
            return historyList ?? Enumerable.Empty<PurchaseHistory>();
        }


    }
}
