using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.HoaDon;
using Repositories.Interfaces;

namespace Restaurant_API.Controllers
{
    [Route("HoaDon")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonServices _hoaDonServices;
        public HoaDonController(IHoaDonServices hoaDonServices)
        {
            _hoaDonServices = hoaDonServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHoaDonRequest request)
        {
            var result = await _hoaDonServices.Create(request);
            return Ok(result);
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _hoaDonServices.Delete(Id);
            return Ok(result);
        }
      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHoaDon(Guid id)
        {
            var result = await _hoaDonServices.GetById(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]GetHoaDonRequest request)
        {
            var result = await _hoaDonServices.GetAll(request);
            return Ok(result);
        }
		[HttpPost("doanh-thu-hoa-don")]
		public async Task<IActionResult> DoanhThu([FromBody] GetByDateRequest request)
		{
			var result = await _hoaDonServices.DoanhThuHoaDon(request);
			return Ok(result);
		}
		[HttpGet("doanh-thu-chi-tiet")]
		public async Task<IActionResult> DoanhThuChiTiet(DateTime request)
		{
			var result = await _hoaDonServices.DoanhThuChiTiet(request);
			return Ok(result);
		}
		[HttpGet("chi-tiet-hoa-don")]
		public async Task<IActionResult> GetByDate(Guid mahoadon)
		{
			var result = await _hoaDonServices.HoaDonChiTiet(mahoadon);
			return Ok(result);
		}
        [HttpGet("thanh-toan")]
        public async Task<IActionResult> ThanhToan(long id)
		{
			var result = await _hoaDonServices.ThanhToan(id);
			return Ok(result); 
		}
	}
}
