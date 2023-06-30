using Application.Dto.Request;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/calculation")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly ICalculationService _calculatorService;
        public CalculationController(ICalculationService calculationService)
        {
            _calculatorService = calculationService;
        }
        [Route("calculate-foot-print")]
        [HttpPost]
        public async Task<IActionResult> CalculateFootPrintAsync(GetCalculationRequest getCalculationRequest)
        {
            var result = await _calculatorService.CalculateFootPrintAsync(getCalculationRequest);
            //var user = await _userService.LoginAsync(loginUserRequest);
            //var user = await _authService.LoginAsync(loginUserRequest);
            return Ok(result);
        }
    }
}
