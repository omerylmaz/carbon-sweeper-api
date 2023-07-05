using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants
{
    public static class UserCalculationWarningsMessages
    {
        public static string UserElectricityBad(decimal value)
        {
            return $"Your electricity foot print value {value}. It is too much, you have to decrease it.";
        }
        public static string UserElectricityPerfect(decimal value)
        {
            return $"Your electricity foot print value {value}. It is okey according to avarage user, you should keep it.";
        }

        public static string UserCoalBad(decimal value)
        {
            return $"Your coal foot print value {value}. You should not use coal, you must leave it.";
        }
        public static string UserCoalPerfect(decimal value)
        {
            return $"Your coal foot print value {value}. It is good not to use.";
        }
        public static string UserLpgBad(decimal value)
        {
            return $"Your lpg foot print value {value}. It is too much, you have to decrease it.";
        }
        public static string UserLPGPerfect(decimal value)
        {
            return $"Your lpg foot print value {value}. It is okey according to avarage user, you should keep it.";
        }

        public static string UserCarFuelBad(decimal value)
        {
            return $"Your car fuel foot print value {value}. It is too much, you have to decrease it.";
        }
        public static string UserCarFuelPerfect(decimal value)
        {
            return $"Your car fuel foot print value {value}. It is okey according to avarage user, you should keep it.";
        }
        public static string UserPublicTransportBad(decimal value)
        {
            return $"Your public transport foot print value {value}. It is too much, you have to decrease it.";
        }
        public static string UserPublicTransportPerfect(decimal value)
        {
            return $"Your public transport foot print value {value}. It is okey according to avarage user, you should keep it.";
        }
        public static string UserPaperProductBad(decimal value)
        {
            return $"Your public transport foot print value {value}. It is too much, you have to decrease it.";
        }
        public static string UserPaperProductPerfect(decimal value)
        {
            return $"Your paper product foot print value {value}. It is okey according to avarage user, you should keep it.";
        }
        public static string UserDressingBad(decimal value)
        {
            return $"Your public dressing foot print value {value}. It is too much, you have to decrease it.";
        }
        public static string UserDressingPerfect(decimal value)
        {
            return $"Your dressing foot print value {value}. It is okey according to avarage user, you should keep it.";
        }
        public static string UserElectronicsBad(decimal value)
        {
            return $"Your electronics foot print value {value}. It is too much, you have to decrease it.";
        }
        public static string UserElectronicsPerfect(decimal value)
        {
            return $"Your electronics foot print value {value}. It is okey according to avarage user, you should keep it.";
        }
        public static string UserFoodBad(decimal value)
        {
            return $"Your food foot print value {value}. It is too much, you have to decrease it.";
        }
        public static string UserFoodPerfect(decimal value)
        {
            return $"Your food foot print value {value}. It is okey according to avarage user, you should keep it.";
        }
        public static string UserFunBad(decimal value)
        {
            return $"Your fun foot print value {value}. It is too much, you have to decrease it.";
        }
        public static string UserFunPerfect(decimal value)
        {
            return $"Your fun foot print value {value}. It is okey according to avarage user, you should keep it.";
        }
    }
}
