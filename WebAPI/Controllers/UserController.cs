using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(
            IUserService userService
            )
        {
            _userService = userService;
        }
        [Route("foot-prints")]
        [HttpGet]
        public async Task<IActionResult> GetUsersFootPrintAsync()
        {
            var response = await _userService.GetUsersFootPrintAsync();
            return Ok(response);
        }

        [Route("welcome")]
        [HttpGet]
        public async Task<IActionResult> GetUsersWelcomeDataAsync()
        {
            var response = await _userService.GetUsersWelcomeDataAsync();
            return Ok(response);
        }

        [Route("info")]
        [HttpGet]
        public async Task<IActionResult> GetUserInfos()
        {
            var response = await _userService.GetUserInfos();
            return Ok(response);
        }
    }
}
