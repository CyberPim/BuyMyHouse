using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class House : IEntityBase
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Place { get; set; }
        public string ZipCode { get; set; }
        public double Price { get; set; }
        public string ImgUrl { get; set; }
        public HouseStatus Status { get; set; }
    }
}
