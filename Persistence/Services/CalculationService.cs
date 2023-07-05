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
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

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
        private readonly IHttpContextAccessor _httpContextAccessor;


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
,
            IHttpContextAccessor httpContextAccessor
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
            _httpContextAccessor = httpContextAccessor;
            //house
        }
        public async Task<decimal> CalculateFootPrintAsync(GetCalculationRequest getCalculationRequest)
        {
            HttpContext context = _httpContextAccessor.HttpContext; //tekrarlı yapı solide uygun değil bir helper yapabilirsin bunun için
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;

            //var user = await _userReadRepo.GetByIdAsync(getCalculationRequest.UserId);
            var user = await _userReadRepo.GetSingleAsync(x => (int)x.Id == int.Parse(userId));


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
                    publicTransportFootPrint = 0;
                    break;
                case (short)UsageTransportFrequency.Rarely:
                    publicTransportFootPrint = 100;
                    break;
                case (short)UsageTransportFrequency.Sometimes:
                    publicTransportFootPrint = 250;
                    break;
                case (short)UsageTransportFrequency.Often:
                    publicTransportFootPrint = 500;
                    break;
                case (short)UsageTransportFrequency.Always:
                    publicTransportFootPrint = 750;
                    break;
                default:
                    break;
            }



            var house = await _houseReadRepo.GetSingleAsync(x => x.UserId == user.Id);
            if (house == null)
            {
                house = new House();
                house.Coal = coalFootPrint;
                house.LPG = lpgFootPrint;
                house.Electricity = electricityFootPrint;
                house.UserId = int.Parse(userId);
                await _houseWriteRepo.AddAsync(house);

            }
            else
            {
                house.Coal = coalFootPrint;
                house.LPG = lpgFootPrint;
                house.Electricity = electricityFootPrint;
                house.UserId = int.Parse(userId);
            }
            await _houseWriteRepo.SaveChangesAsync();

            var generalConsumption = await _consumptionReadRepo.GetSingleAsync(x => x.UserId == user.Id);

            if (generalConsumption == null)
            {
                generalConsumption = new GeneralConsumption();
                generalConsumption.UserId = int.Parse(userId);
                await _consumptionWriteRepo.AddAsync(generalConsumption);
            }

            generalConsumption.PaperProductFootPrint = paperProductFootPrint;
            generalConsumption.FoodFootPrint = foodFootPrint;
            generalConsumption.DressingFootPrint = dressingFootPrint;
            generalConsumption.FunFootPrint = funFootPrint;
            generalConsumption.ElectronicsFootPrint = electronicsFootPrint;

            await _consumptionWriteRepo.SaveChangesAsync();

            var transport = await _transportReadRepo.GetSingleAsync(x => x.UserId == user.Id);
            if (transport == null)
            {
                transport = new Transport();
                transport.UserId = int.Parse(userId);
                await _transportWriteRepo.AddAsync(transport);
            }
            transport.CarFootPrint = carFuelFootPrint;
            transport.PublicTransportFootPrint = publicTransportFootPrint;
            transport.UserId = int.Parse(userId);
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

        public async Task<GetFootPrintWarningListResponse> GetFootPrintWarnings()
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            int userId = int.Parse(jwtToken.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value);
            var user = await _userReadRepo.GetSingleAsync(x => x.Id == userId);
            var transport = await _transportReadRepo.GetSingleAsync(x => x.UserId == userId);
            var consumption = await _consumptionReadRepo.GetSingleAsync(x => x.UserId == userId);
            var house = await _houseReadRepo.GetSingleAsync(x => x.UserId == userId);
            var messages = new List<GetFootPrintWarningResponse>();


            if (house.LPG > FootPrintLimits.LPG) 
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserLpgBad((decimal)(house.LPG)) , IsSuccess = false});
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserLPGPerfect((decimal)(house.LPG)), IsSuccess = true });
            }

            // Electricity
            if (house.Electricity > FootPrintLimits.Electricity)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserElectricityBad((decimal)(house.Electricity)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserElectricityPerfect((decimal)(house.Electricity)), IsSuccess = true });
            }

            // Coal
            if (house.Coal > FootPrintLimits.Coal)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserCoalBad((decimal)(house.Coal)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserCoalPerfect((decimal)(house.Coal)), IsSuccess = true });
            }

            // Electronics
            if (consumption.ElectronicsFootPrint > FootPrintLimits.Electronics)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserElectronicsBad((decimal)(consumption.ElectronicsFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserElectronicsPerfect((decimal)(consumption.ElectronicsFootPrint)), IsSuccess = true });
            }

            // Paper Product
            if (consumption.PaperProductFootPrint > FootPrintLimits.PaperProduct)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserPaperProductBad((decimal)(consumption.PaperProductFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserPaperProductPerfect((decimal)(consumption.PaperProductFootPrint)), IsSuccess = true });
            }

            // Dressing
            if (consumption.DressingFootPrint > FootPrintLimits.Dressing)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserDressingBad((decimal)(consumption.DressingFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserDressingPerfect((decimal)(consumption.DressingFootPrint)), IsSuccess = true });
            }

            // Fun
            if (consumption.FunFootPrint > FootPrintLimits.Fun)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserFunBad((decimal)(consumption.FunFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserFunPerfect((decimal)(consumption.FunFootPrint)), IsSuccess = true });
            }

            // Food
            if (consumption.FoodFootPrint > FootPrintLimits.Food)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserFoodBad((decimal)(consumption.FoodFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserFoodPerfect((decimal)(consumption.FoodFootPrint)), IsSuccess = true });
            }

            // Car Fuel
            if (transport.CarFootPrint > FootPrintLimits.Car)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserCarFuelBad((decimal)(transport.CarFootPrint)), IsSuccess = false });
            }
            else
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserCarFuelPerfect((decimal)(transport.CarFootPrint)), IsSuccess = true });
            }

            //// Public Transport
            //if (transport.PublicTransportFootPrint > FootPrintLimits.PublicTransport)
            //{
            //    messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserPublicTransportBad((decimal)(transport.PublicTransportFootPrint)), IsSuccess = false });
            //}
            //else
            //{
            //    messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserPublicTransportPerfect((decimal)(transport.PublicTransportFootPrint)), IsSuccess = true });
            //}

            if (user.FootPrintReduction < 0)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserReductionPerfect(), IsSuccess = true });
            }
            else if(user.FootPrintReduction > 0)
            {
                messages.Add(new GetFootPrintWarningResponse() { Message = UserCalculationWarningsEncouragementMessages.UserReductionBad(), IsSuccess = false });
            }

            return new GetFootPrintWarningListResponse() { Messages = messages };

        }
        //ctrl + k + u uncomment
        //ctrl + k + d lind fix
        //ctrl + k + c commend 
    }
}
