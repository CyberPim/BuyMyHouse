using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterface
{
    public interface IBuyerService
    {
        Task<Buyer> AddBuyer(Buyer buyer);
        Task<Buyer> GetBuyer(int id);
        Task<Buyer> UpdateBuyer(int id, Buyer data);
        Task<string> DeleteBuyer(int id);
    }
}
