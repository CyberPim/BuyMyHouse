using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterface
{
    public interface IHouseService
    {
        Task<House> AddHouse(House house);
        Task<House> GetHouse(int id);
        Task<List<House>> GetAll();
        Task<House> UpdateHouse(int id, House data);
        Task<string> DeleteHouse(int id);
    }
}
