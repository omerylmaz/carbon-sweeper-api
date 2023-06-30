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
            IWriteRepo<User> userWriteRepo
            //house

            )
        {
            _userWriteRepo = userWriteRepo;
            _userReadRepo = userReadRepo;
            //house
        }
        public async Task<decimal> CalculateFootPrintAsync(GetCalculationRequest getCalculationRequest)
        {
            //var user = await _userReadRepo.GetByIdAsync(getCalculationRequest.UserId);
            var user = await _userReadRepo.GetSingleAsync(x => (int)x.Id == (int)getCalculationRequest.UserId);

            var electricityFootPrint = getCalculationRequest.ElectricityTl * 1.5M;
            var coalFootPrint = getCalculationRequest.CoalTl * 1.5M;
            var total = electricityFootPrint + coalFootPrint;
            var house = new House();
            house.Coal = coalFootPrint;
            await _houseWriteRepo.AddAsync(house);

            user.FootPrint = total;
            await _userWriteRepo.SaveChangesAsync();
            throw new NotImplementedException();
        }
        //ctrl + k + u uncomment
        //ctrl + k + d lind fix
        //ctrl + k + c commend 
    }
}
