using Application.Dto.Response;
using Application.Repos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IReadRepo<User> _userReadRepo;
        private readonly IMapper _mapper;
        public UserService(
            IReadRepo<User> userReadRepo,
            IMapper mapper
            )
        {
            _userReadRepo = userReadRepo;
            _mapper = mapper;
        }
        public async Task<GetUserFootPrintsListResponse> GetUsersFootPrintAsync()
        {
            var usersDb = await _userReadRepo.GetWhere(x => x.FootPrint > 0).OrderBy(x => x.FootPrint).ToListAsync();
            try
            {

            var usersResponse = _mapper.Map<List<GetUserFootPrintResponse>>(usersDb);
            return new GetUserFootPrintsListResponse() { UserFootPrintList = usersResponse };
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<GetUsersWelcomeDataResponse> GetUsersWelcomeDataAsync()
        {
            var users = await _userReadRepo.GetAll().ToListAsync();
            var userCount = users.Count();
            var footPrintTotal = users.Sum(user => user.FootPrint);
            var welcomeUserData = new GetUsersWelcomeDataResponse() { TotalUserAmount = users.Count(), SavedTrees = (int)footPrintTotal / 20, TotalFootPrint = footPrintTotal };
            return welcomeUserData;
        }
    }
}
