
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.Authe;
using Repository.Interfaces;
using Restaurant_API.Middleware;

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
        [HttpPost]
        public async Task<IActionResult> Login(LoginParam param)
        {
            var result = await _authenticationService.Login(param);
            return Ok(result);
        }
        [HttpPost("refresh-token")]
		[Authorize]
		public async Task<IActionResult> RefreshToken()
        {
			var result = await _authenticationService.RefreshToken();
			return Ok(result);
		}
    }
}
