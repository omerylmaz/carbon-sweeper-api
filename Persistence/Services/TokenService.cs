using Application.Dto.Response;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Persistence.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        //private readonly UserManager<AppUser> _userManager;


        public TokenService(IConfiguration configuration /*UserManager<AppUser> userManager*/)
        {
            //_userManager = userManager;
            _configuration = configuration;
        }

        public TokenResponse CreateAccessToken(int minute, List<Claim> claims = null)
        {
            TokenResponse tokenResponse = new TokenResponse();

            if (claims == null)
            {
                claims = new List<Claim>();
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            tokenResponse.ExpirationDate = DateTime.UtcNow.AddMinutes(minute);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                expires: tokenResponse.ExpirationDate,
                signingCredentials: signingCredentials,
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                notBefore: DateTime.UtcNow
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenResponse.AccessToken = tokenHandler.WriteToken(securityToken);

            tokenResponse.RefreshToken = CreateRefreshToken();

            return tokenResponse;
        }


        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
