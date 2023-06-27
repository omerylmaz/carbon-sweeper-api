using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GeneralConsumption : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<User> Users { get; set; }
    }
}
