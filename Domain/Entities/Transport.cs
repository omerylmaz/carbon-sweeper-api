using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transport : BaseEntity
    {
        public decimal Mileage { get; set; }
        public string Type { get; set; }
        public List<User> Users { get; set; }
    }
}
