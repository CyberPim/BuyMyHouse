using DAL.Repository;
using DAL.Repository.Interface;
using Domain;
using Service.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MortgageService : IMortgageService
    {
        private readonly IMortgageRepository _mortgageRepository;
        
        public MortgageService(IMortgageRepository mortgageRepository)
        {
            _mortgageRepository = mortgageRepository;
        }

        public async Task<Mortgage> AddMortage(Mortgage mortgage)
        {
            try
            {
                _mortgageRepository.Add(mortgage);
                _mortgageRepository.Commit();
                return mortgage;
            }
            catch (Exception)
            {
                throw new Exception("Mortgage already exist");
            }
        }

        public Task<Mortgage> GetMortgage(int id)
        {
            Mortgage mortgage = _mortgageRepository.GetSingle(id);
            return Task.FromResult(mortgage);
        }
        
        public async Task<List<Mortgage>> GetMortgages()
        {
            IEnumerable<Mortgage> mortgages = _mortgageRepository.GetAll();
            return mortgages.ToList();
        }
        
        public Task<List<Mortgage>> GetMortgagesByIssuedDate()
        {
            IEnumerable<Mortgage> mortgages = _mortgageRepository.FindBy(m => m.DateIssued >= DateTime.Now.Subtract(TimeSpan.FromDays(1)));
            return Task.FromResult((List<Mortgage>)mortgages);
        }

        public async Task<Mortgage> UpdateMortgage(int id, Mortgage data)
        {
            Mortgage mortgage = await GetMortgage(id);

            if (mortgage == null)
            {
                throw new Exception("Mortgage does not exist");
            }

            _mortgageRepository.Detach(data);
            _mortgageRepository.Update(data);
            _mortgageRepository.Commit();
            return data;
        }
    }
}
