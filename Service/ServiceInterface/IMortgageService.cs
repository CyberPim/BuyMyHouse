using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterface
{
    public interface IMortgageService
    {
        Task<Mortgage> AddMortage(Mortgage mortgage);
        Task<Mortgage> GetMortgage(int id);
        Task<List<Mortgage>> GetMortgages();
        Task<Mortgage> UpdateMortgage(int id, Mortgage data);
        Task<List<Mortgage>> GetMortgagesByIssuedDate();

    }
}
