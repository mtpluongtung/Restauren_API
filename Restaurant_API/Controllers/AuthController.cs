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
      
    }
}
