using Application.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ITokenService
    {
        TokenResponse CreateAccessToken(int minute, List<Claim> claims = null);

        string CreateRefreshToken();
    }
}
