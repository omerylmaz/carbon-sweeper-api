using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Request
{
    public class GetCalculationRequest
    {
        //ton
        //public int UserId { get; set; }
        public decimal ElectricityTl { get; set; } 
        public decimal CoalTl { get; set; }
        public decimal LPGTl { get; set; }
        public decimal CarFuelTl { get; set; }
        public short TransportFrequency { get; set; }
        public decimal DressingTl { get; set; }
        public short DietType { get; set; }
        public decimal FoodTl { get; set; }
        public decimal ElectronicsTl { get; set; }
        public decimal PaperProductTl { get; set; }
        public decimal FunTl { get; set; }
    }
    public enum UsageTransportFrequency
    {
        Never = 1,
        Rarely = 2,
        Sometimes = 3,
        Often = 4,
        Always = 5,
    }
    public enum Diet
    {
        Vegan = 1,
        Vegetarian = 2,
        Pescatarian = 3,
        LittleMeat = 4,
        MediumMeat = 5,
        HardMeat = 6
    }
}
