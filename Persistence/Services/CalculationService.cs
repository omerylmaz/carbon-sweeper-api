using Application.Constants;
using Application.Dto.Request;
using Application.Dto.Response;
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
        private readonly IReadRepo<GeneralConsumption> _consumptionReadRepo;
        private readonly IWriteRepo<GeneralConsumption> _consumptionWriteRepo;
        private readonly IReadRepo<Transport> _transportReadRepo;
        private readonly IWriteRepo<Transport> _transportWriteRepo;


        public CalculationService(
            IReadRepo<User> userReadRepo,
            IWriteRepo<User> userWriteRepo,
            IReadRepo<House> houseReadRepo,
            IWriteRepo<House> houseWriteRepo
,
            IReadRepo<GeneralConsumption> consumptionReadRepo,
            IWriteRepo<GeneralConsumption> consumptionWriteRepo,
            IReadRepo<Transport> transportReadRepo,
            IWriteRepo<Transport> transportWriteRepo
            //house

            )
        {
            _userWriteRepo = userWriteRepo;
            _userReadRepo = userReadRepo;
            _houseWriteRepo = houseWriteRepo;
            _houseReadRepo = houseReadRepo;
            _consumptionReadRepo = consumptionReadRepo;
            _consumptionWriteRepo = consumptionWriteRepo;
            _transportReadRepo = transportReadRepo;
            _transportWriteRepo = transportWriteRepo;
            //house
        }
        public async Task<decimal> CalculateFootPrintAsync(GetCalculationRequest getCalculationRequest)
        {
            //var user = await _userReadRepo.GetByIdAsync(getCalculationRequest.UserId);
            var user = await _userReadRepo.GetSingleAsync(x => (int)x.Id == (int)getCalculationRequest.UserId);


            var electricityFootPrint = getCalculationRequest.ElectricityTl * 0.8M * 12;
            var coalFootPrint = getCalculationRequest.CoalTl * 0.8M * 12;
            var lpgFootPrint = getCalculationRequest.LPGTl * 0.4M * 12;
            var carFuelFootPrint = getCalculationRequest.CarFuelTl * 0.5M * 12;
            var dressingFootPrint = getCalculationRequest.DressingTl * 0.03M * 12;
            var electronicsFootPrint = getCalculationRequest.ElectronicsTl * 0.05M * 12;
            var paperProductFootPrint = getCalculationRequest.PaperProductTl * 0.03M * 12;
            var funFootPrint = getCalculationRequest.FunTl * 0.01M * 12;
            var foodFootPrint = 0M;
            switch (getCalculationRequest.DietType)
            {
                case (short)Diet.Vegan: // Vegan
                    foodFootPrint = getCalculationRequest.FoodTl * 0.02M * 12;
                    break;
                case (short)Diet.Vegetarian: // Vegetarian
                    foodFootPrint = getCalculationRequest.FoodTl * 0.03M * 12;
                    break;
                case (short)Diet.Pescatarian: // Pescatarian
                    foodFootPrint = getCalculationRequest.FoodTl * 0.04M * 12;
                    break;
                case (short)Diet.LittleMeat: // Little Meat
                    foodFootPrint = getCalculationRequest.FoodTl * 0.05M * 12;
                    break;
                case (short)Diet.MediumMeat: // Medium Meat
                    foodFootPrint = getCalculationRequest.FoodTl * 0.07M * 12;
                    break;
                case (short)Diet.HardMeat: // Hard Meat
                    foodFootPrint = getCalculationRequest.FoodTl * 0.09M * 12;
                    break;
                default:
                    break;
            }
            var publicTransportFootPrint = 0M;
            switch (getCalculationRequest.TransportFrequency)
            {
                case (short)UsageTransportFrequency.Never:
                    publicTransportFootPrint = 0M;
                    break;
                case (short)UsageTransportFrequency.Rarely:
                    publicTransportFootPrint = 0.1M;
                    break;
                case (short)UsageTransportFrequency.Sometimes:
                    publicTransportFootPrint = 0.25M;
                    break;
                case (short)UsageTransportFrequency.Often:
                    publicTransportFootPrint = 0.50M;
                    break;
                case (short)UsageTransportFrequency.Always:
                    publicTransportFootPrint = 0.75M;
                    break;
                default:
                    break;
            }



            var house = await _houseReadRepo.GetSingleAsync(x => x.UserId == getCalculationRequest.UserId);
            if (house == null)
            {
                house = new House();
                house.Coal = coalFootPrint;
                house.LPG = lpgFootPrint;
                house.Electricity = electricityFootPrint;
                house.UserId = getCalculationRequest.UserId;
                await _houseWriteRepo.AddAsync(house);

            }
            else
            {
                house.Coal = coalFootPrint;
                house.LPG = lpgFootPrint;
                house.Electricity = electricityFootPrint;
                house.UserId = getCalculationRequest.UserId;
            }
            await _houseWriteRepo.SaveChangesAsync();

            var generalConsumption = await _consumptionReadRepo.GetSingleAsync(x => x.UserId == getCalculationRequest.UserId);

            if (generalConsumption == null)
            {
                generalConsumption = new GeneralConsumption();
                generalConsumption.UserId = getCalculationRequest.UserId;
                await _consumptionWriteRepo.AddAsync(generalConsumption);
            }

            generalConsumption.PaperProductFootPrint = paperProductFootPrint;
            generalConsumption.FoodFootPrint = foodFootPrint;
            generalConsumption.DressingFootPrint = dressingFootPrint;
            generalConsumption.FunFootPrint = funFootPrint;
            generalConsumption.ElectronicsFootPrint = electronicsFootPrint;

            await _consumptionWriteRepo.SaveChangesAsync();

            var transport = await _transportReadRepo.GetSingleAsync(x => x.UserId == getCalculationRequest.UserId);
            if (transport == null)
            {
                transport = new Transport();
                transport.UserId = getCalculationRequest.UserId;
                await _transportWriteRepo.AddAsync(transport);
            }
            transport.CarFootPrint = carFuelFootPrint;
            transport.PublicTransportFootPrint = publicTransportFootPrint;
            transport.UserId = getCalculationRequest.UserId;
            await _transportWriteRepo.SaveChangesAsync();

            var total = electricityFootPrint + coalFootPrint + lpgFootPrint + carFuelFootPrint + dressingFootPrint + electronicsFootPrint + paperProductFootPrint + funFootPrint + foodFootPrint + publicTransportFootPrint;
            //var reduction = user.FootPrintReduction / total;
            if (user.FootPrint != 0)
                user.FootPrintReduction += user.FootPrint - total;
            user.FootPrint = total;

            await _userWriteRepo.SaveChangesAsync();
            return total;

            //user.FootPrint = total;
        }

        public async Task<GetFootPrintWarningListResponse> GetFootPrintWarnings(int userId)
        {
            var user = await _userReadRepo.GetSingleAsync(x => x.Id == userId);
            var transport = await _transportReadRepo.GetSingleAsync(x => x.UserId == userId);
            var consumption = await _consumptionReadRepo.GetSingleAsync(x => x.UserId == userId);
            var house = await _houseReadRepo.GetSingleAsync(x => x.UserId == userId);
            var messages = new List<GetFootPrintWarningResponse>();


            if (house.LPG > FootPrintLimits.LPG) 
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserLpgBad((decimal)(house.LPG)) , IsSuccess = false});
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserLPGPerfect((decimal)(house.LPG)), IsSuccess = true });
            }

            // Electricity
            if (house.Electricity > FootPrintLimits.Electricity)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserElectricityBad((decimal)(house.Electricity)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserElectricityPerfect((decimal)(house.Electricity)), IsSuccess = true });
            }

            // Coal
            if (house.Coal > FootPrintLimits.Coal)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserCoalBad((decimal)(house.Coal)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserCoalPerfect((decimal)(house.Coal)), IsSuccess = true });
            }

            // Electronics
            if (consumption.ElectronicsFootPrint > FootPrintLimits.Electronics)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserElectronicsBad((decimal)(consumption.ElectronicsFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserElectronicsPerfect((decimal)(consumption.ElectronicsFootPrint)), IsSuccess = true });
            }

            // Paper Product
            if (consumption.PaperProductsFootPrint > FootPrintLimits.PaperProducts)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserPaperProductsBad((decimal)(consumption.PaperProductsFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserPaperProductsPerfect((decimal)(consumption.PaperProductsFootPrint)), IsSuccess = true });
            }

            // Dressing
            if (consumption.DressingFootPrint > FootPrintLimits.Dressing)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserDressingBad((decimal)(consumption.DressingFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserDressingPerfect((decimal)(consumption.DressingFootPrint)), IsSuccess = true });
            }

            // Fun
            if (consumption.FunFootPrint > FootPrintLimits.Fun)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserFunBad((decimal)(consumption.FunFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserFunPerfect((decimal)(consumption.FunFootPrint)), IsSuccess = true });
            }

            // Food
            if (consumption.FoodFootPrint > FootPrintLimits.Food)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserFoodBad((decimal)(consumption.FoodFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserFoodPerfect((decimal)(consumption.FoodFootPrint)), IsSuccess = true });
            }

            // Car Fuel
            if (transportation.CarFuel > FootPrintLimits.CarFuel)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserCarFuelBad((decimal)(transportation.CarFuel)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserCarFuelPerfect((decimal)(transportation.CarFuel)), IsSuccess = true });
            }

            // Public Transport
            if (transportation.PublicTransport > FootPrintLimits.PublicTransport)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserPublicTransportBad((decimal)(transportation.PublicTransport)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsMessages.UserPublicTransportPerfect((decimal)(transportation.PublicTransport)), IsSuccess = true });
            }

            return new GetFootPrintWarningListResponse() { Messages = messages };

        }
        //ctrl + k + u uncomment
        //ctrl + k + d lind fix
        //ctrl + k + c commend 
    }
}
