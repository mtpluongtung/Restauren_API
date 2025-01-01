using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.MonAn;
using Models.DTO.Request.NhanVien;
using Repositories.Interfaces;

namespace Restaurant_API.Controllers
{
    [Route("NhanVien")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
		private readonly INhanVienServices _nhanVienServices;
		public NhanVienController(INhanVienServices nhanVienServices)
		{
			_nhanVienServices = nhanVienServices;
		}

		[HttpGet("CheckIn")]
		public async Task<IActionResult> CheckIn(string token)
		{
			var result = await _nhanVienServices.CheckIn(token);
			return Ok(result);
		}

		[HttpGet("CheckOut")]
		public async Task<IActionResult> CheckOut(string token)
		{
			var result = await _nhanVienServices.CheckOut(token);
			return Ok(result);
		}

		[HttpGet("GenUrlCheckIn")]
		public async Task<IActionResult> GenUrlCheckIn(string token)
		{
			var result = await _nhanVienServices.GenCheckIn(token);
			return Ok(result);
		}
		[HttpGet("GenUrlCheckOut")]
		public async Task<IActionResult> GenUrlCheckOut(string token)
		{
			var result = await _nhanVienServices.GenCheckOut(token);
			return Ok(result);
		}
		[HttpPost]
		public async Task<IActionResult> Create(UpdateNhanVienRequest request)
		{
			var reslut = await _nhanVienServices.CreateOrUpdate(request);
			return Ok(reslut);
		}
		
		[HttpGet("GetChamCong")]
		public async Task<IActionResult> GetChamCong()
		{
			var result = await _nhanVienServices.GetChamCong();
			return Ok(result);
		}
		[HttpGet()]
		public async Task<IActionResult> GetAll([FromQuery] BaseSearchRequest request)
		{
			var result = await _nhanVienServices.GetNhanVien(request);
			return Ok(result);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var result = await _nhanVienServices.Delete(id);
			return Ok(result);
		}
	}
}
