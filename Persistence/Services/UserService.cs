using Application.Dto.Response;
using Application.Repos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IReadRepo<User> _userReadRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IReadRepo<User> userReadRepo,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor

            )
        {
            _userReadRepo = userReadRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<GetUserResponse> GetUserInfos()
        {
            HttpContext context = _httpContextAccessor.HttpContext; //tekrarlı yapı solide uygun değil bir helper yapabilirsin bunun için
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            int userId = int.Parse(jwtToken.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value);
            var user = await _userReadRepo.GetByIdAsync(userId);
            var getUserInfos = new GetUserResponse();
            getUserInfos.FootPrint = $"{user.FootPrint/1000} ton";
            getUserInfos.FootPrintReduction = user.FootPrintReduction;
            getUserInfos.UserName = user.UserName;
            getUserInfos.FirstName = user.FirstName;
            getUserInfos.LastName = user.LastName;
            getUserInfos.Email = user.Email;

            return getUserInfos;
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
            //var footPrintTotal = users.Sum(user => user.FootPrint);
            var reducedFootPrint = users.Sum(user => user.FootPrintReduction);
            var welcomeUserData = new GetUsersWelcomeDataResponse() { TotalUserAmount = users.Count(), SavedTrees = (int)reducedFootPrint / 20, ReducedFootPrint = reducedFootPrint };
            return welcomeUserData;
        }
    }
}
