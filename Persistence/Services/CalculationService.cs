using Application.Dto.Request;
using Application.Repos;
using Application.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IReadRepo<User> _userReadRepo;
        private readonly IWriteRepo<User> _userWriteRepo;
        private readonly IReadRepo<House> _houseReadRepo;
        private readonly IWriteRepo<House> _houseWriteRepo;


        public CalculationService(
            IReadRepo<User> userReadRepo,
            IWriteRepo<User> userWriteRepo,
            IReadRepo<House> houseReadRepo,
            IWriteRepo<House> houseWriteRepo
            //house

            )
        {
            _userWriteRepo = userWriteRepo;
            _userReadRepo = userReadRepo;
            _houseWriteRepo = houseWriteRepo;
            _houseReadRepo = houseReadRepo;
            //house
        }
        public async Task<decimal> CalculateFootPrintAsync(GetCalculationRequest getCalculationRequest)
        {
            //var user = await _userReadRepo.GetByIdAsync(getCalculationRequest.UserId);
            var user = await _userReadRepo.GetSingleAsync(x => (int)x.Id == (int)getCalculationRequest.UserId);

            var electricityFootPrint = getCalculationRequest.ElectricityTl * 1.5M * 12;
            var coalFootPrint = getCalculationRequest.CoalTl * 3M * 12;
            var lpgFootPrint = getCalculationRequest.LPGTl * 2M * 12;
            var carFuelFootPrint = getCalculationRequest.CarFuelTl * 1.5M * 12;
            var dressingFootPrint = getCalculationRequest.DressingTl * 0.03M * 12;
            var electronicsFootPrint = getCalculationRequest.ElectronicsTl * 0.05M * 12;
            var paperProdcutFootPrint = getCalculationRequest.PaperProductTl * 0.03M * 12;
            var foodFootPrint = 0M;
            switch (getCalculationRequest.DietType)
            {
                case 1: // Vegan
                    foodFootPrint = getCalculationRequest.FoodTl * 0.02M * 12;
                    break;
                case 2: // Vegetarian
                    foodFootPrint = getCalculationRequest.FoodTl * 0.03M * 12;
                    break;
                case 3: // Pescatarian
                    foodFootPrint = getCalculationRequest.FoodTl * 0.04M * 12;
                    break;
                case 4: // Little Meat
                    foodFootPrint = getCalculationRequest.FoodTl * 0.05M * 12;
                    break;
                case 5: // Medium Meat
                    foodFootPrint = getCalculationRequest.FoodTl * 0.07M * 12;
                    break;
                case 6: // Hard Meat
                    foodFootPrint = getCalculationRequest.FoodTl * 0.09M * 12;
                    break;
                default:
                    break;
            }
        

            var house = new House();
            house.Coal = coalFootPrint;
            house.LPG = lpgFootPrint;
            house.Electricity = electricityFootPrint;
            await _houseWriteRepo.AddAsync(house);

            
            await _userWriteRepo.SaveChangesAsync();
            throw new NotImplementedException();

            //var total = electricityFootPrint + coalFootPrint;
            //user.FootPrint = total;
        }
        //ctrl + k + u uncomment
        //ctrl + k + d lind fix
        //ctrl + k + c commend 
    }
}
