using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.MonAn;
using Models.DTO.Request.Set;
using Repositories.Interfaces;

namespace Restaurant_API.Controllers
{
	[Route("Set")]
	[ApiController]
	public class SetController : ControllerBase
	{
		private readonly ISetServices _setServices;
		public SetController(ISetServices services)
		{
			_setServices = services;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseSearchRequest request)
		{
			var reuslt = await _setServices.GetAll(request);
			return Ok(reuslt);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(long id)
		{
			var result = await _setServices.GetById(id);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] CreateSetRequest request)
		{
			try
			{
				var result = await _setServices.Create(request);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(long Id)
		{
			var result = await _setServices.Delete(Id);
			return Ok(result);
		}
	}
}
