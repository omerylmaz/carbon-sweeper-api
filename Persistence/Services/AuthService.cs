using Application.Dto.Request;
using Application.Dto.Response;
using Application.Repos;
using Application.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IWriteRepo<User> _userWriteRepo;
        private readonly IReadRepo<User> _userReadRepo;
        private readonly ITokenService _tokenService;

        public AuthService(
            IWriteRepo<User> userWriteRepo,
            IReadRepo<User> userReadRepo,
            ITokenService tokenService
            )
        {
            _userWriteRepo = userWriteRepo;
            _userReadRepo = userReadRepo;
            _tokenService = tokenService;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest loginUserRequest)
        {
            var user = await _userReadRepo.GetSingleAsync(x => x.UserName == loginUserRequest.UserNameOrEmail);
            if (user == null)
            {
                user = await _userReadRepo.GetSingleAsync(x => x.Email == loginUserRequest.UserNameOrEmail);
                if (user == null)
                    throw new Exception("User is not found!!!");
            }
            if (loginUserRequest.Password != user.Password)
                throw new Exception("Password is wrong!!!");

            TokenResponse tokenResponse = new();
            var claims = new List<Claim>()
                    {
                        new Claim("role", "User"),
                    };
            tokenResponse = _tokenService.CreateAccessToken(claims: claims, minute: 1000);

            //user.Id = user.Id;
            //user.UserName = user.UserName;
            //user.Password = user.Password;
            user.AccessToken = tokenResponse.AccessToken;
            //user.Email = user.Email;
            user.ExpirationDate = tokenResponse.ExpirationDate;
            //user.FirstName = user.FirstName;
            //user.LastName = user.LastName;
            //user.HouseId = user.HouseId;
            user.RefreshToken = tokenResponse.RefreshToken;
            //user.UserRoleId = user.UserRoleId;
            //user.Transports = user.Transports;
            //user.GeneralConsumptions = user.GeneralConsumptions;
            //user.House = user.House;
            //_userWriteRepo.Update(userUpdate);
            await _userWriteRepo.SaveChangesAsync();
            return new LoginResponse() { Token = tokenResponse };
        }
    }
}
