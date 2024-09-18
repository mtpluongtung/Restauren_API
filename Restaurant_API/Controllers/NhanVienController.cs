using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create(CreateNhanVienRequest request)
        {
            var reslut = await _nhanVienServices.Create(request);
            return Ok(reslut);
        }
    }
}
