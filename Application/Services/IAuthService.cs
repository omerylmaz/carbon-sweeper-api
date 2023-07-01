using Application.Dto.Request;
using Application.Dto.Response;
using Application.Repos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginUserRequest);
        Task<bool> RegisterAsync(RegisterRequest registerUserRequest);
    }
}
