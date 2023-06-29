﻿using Application.Dto.Request;
using Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Services;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(
            //IUserService userService, 
            IAuthService authService
            )
        {
            //_userService = userService;
            _authService = authService;
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginRequest loginUserRequest)
        {
            //var user = await _userService.LoginAsync(loginUserRequest);
            var user = await _authService.LoginAsync(loginUserRequest);
            return Ok(user);
        }
    }
}
