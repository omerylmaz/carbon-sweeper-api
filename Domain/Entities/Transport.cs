using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transport : BaseEntity
    {
        public decimal CarFootPrint { get; set; }
        public decimal PublicTransportFootPrint { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        //public List<User> Users { get; set; }
    }
}
