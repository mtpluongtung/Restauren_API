using Entities.DTO.Request.MonAn;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Restaurant_API.Controllers
{
    [Route("MonAn")]
    [ApiController]
    public class MonAnController : ControllerBase
    {
        private readonly IMonAnServices _monAnServices;
        public MonAnController(IMonAnServices monAnServices)
        {
            _monAnServices = monAnServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMonAnRequest request)
        {
            var result = await _monAnServices.Create(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMonAnRequest request)
        {
            var result = await _monAnServices.Update(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _monAnServices.Delete(id);
            return Ok(result);
        }
        [HttpGet]   
        public async Task<IActionResult> GetAll()
        {
            var result = await _monAnServices.GetAll();
            return Ok(result);
        }
    }
}
