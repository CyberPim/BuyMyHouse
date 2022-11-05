using Newtonsoft.Json.Serialization;
using ServiceStack.DataAnnotations;

namespace Domain
{
    public class Buyer : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}