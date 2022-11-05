using DAL.Context;
using DAL.Repository.Interface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BuyerRepository : EntityBaseRepository<Buyer>, IBuyerRepository
    {
        public BuyerRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
