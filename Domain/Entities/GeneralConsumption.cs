using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GeneralConsumption : BaseEntity
    {
        //public string Name { get; set; }
        //public decimal Price { get; set; }
        public decimal DressingFootPrint { get; set; }
        public decimal FoodFootPrint { get; set; }
        public decimal ElectronicsFootPrint { get; set; }
        public decimal PaperProductFootPrint { get; set; }
        public decimal FunFootPrint { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
