using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.CaLamViec;
using Repositories.Interfaces;

namespace Restaurant_API.Controllers
{
	[Route("ca-lam-viec")]
	[ApiController]
	public class CaLamViecController : ControllerBase
	{
		private readonly ICaLamViec _caLamViecServices;
		public CaLamViecController(ICaLamViec caLamViecServices)
		{
			_caLamViecServices = caLamViecServices;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _caLamViecServices.GetAll();
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCaLamViec request)
		{
			var result = await _caLamViecServices.Create(request);
			return Ok(result);
		}
		[HttpPut]
		public async Task<IActionResult> Update(UpdateCaLamViecRequest request)
		{
			var result = await _caLamViecServices.Update(request);
			return Ok(result);
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(long id)
		{
			var result = await _caLamViecServices.Delete(id);
			return Ok(result);
		}
	}
}
