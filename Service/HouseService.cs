using Domain;
using Service.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class HouseService : IHouseService
    {
        public Task<House> AddHouse(House house)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteHouse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<House>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<House> GetHouse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<House> UpdateHouse(int id, House data)
        {
            throw new NotImplementedException();
        }
    }
}
