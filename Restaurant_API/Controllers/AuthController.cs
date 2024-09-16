using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.Authe;
using Repository.Interfaces;

namespace Restaurant_API.Controllers
{
    [Route("Authen")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAutheServices _authenticationService;
        public AuthController(IAutheServices authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost()]
        public async Task<IActionResult> Login(LoginParam param)
        {
            var result = await _authenticationService.Login(param);
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            var result = await _authenticationService.RefreshToken();
            if (result == null)
            {
                return new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
