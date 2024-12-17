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
        [HttpPost("Themmon")]
        public async Task<IActionResult> ThemMon()
        {
            return null;
        }
        [HttpGet("Id")]
        public async Task<IActionResult> GetHoaDon(Guid id)
        {
            var result = await _hoaDonServices.GetById(id);
            return Ok(result);
        }
    }
}
