using DAL.Repository.Interface;
using Domain;
using Microsoft.Extensions.Logging;
using Service.ServiceInterface;

namespace Service
{
    public class BuyerService : IBuyerService
    {
        private readonly ILogger<BuyerService> _logger;
        private readonly IBuyerRepository _buyerRepository;

        public BuyerService(IBuyerRepository buyerRepo, ILogger<BuyerService> logger)
        {
            _buyerRepository = buyerRepo;
            _logger = logger;
        }
        
        public async Task<Buyer> AddBuyer(Buyer buyer)
        {
            try
            {
                _buyerRepository.Add(buyer);
                _buyerRepository.Commit();
                return buyer;
            }
            catch (Exception)
            {
                throw new Exception("Client already exist");
            }
        }

        public Task<string> DeleteBuyer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Buyer> GetBuyer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Buyer> UpdateBuyer(int id, Buyer data)
        {
            throw new NotImplementedException();
        }
    }
}