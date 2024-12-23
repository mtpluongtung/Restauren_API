using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.Bep;
using Repositories.Interfaces;

namespace Restaurant_API.Controllers
{
	[Route("Bep")]
	[ApiController]
	public class BepController : ControllerBase
	{
        private readonly IBepServices _bepServices;
        public BepController(IBepServices bepServices)
        {
			_bepServices = bepServices;

		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] int page, int pageSize)
		{
			var result = await _bepServices.GetAll(page, pageSize);
			return Ok(result);
		}
		[HttpPut]
		public async Task<IActionResult> Update(UpdateBep request)
		{
			await _bepServices.Update(request);
			return Ok();
		}
		
    }
}
