using Entities.DTO.Request.Ban;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.Ban;
using Models.DTO.Request.MonAn;
using Repositories.Interfaces;

namespace Restaurant_API.Controllers
{
    [Route("Ban")]
    [ApiController]
    public class BanController : ControllerBase
    {
        private readonly IBanServices _banService;
        public BanController(IBanServices banService)
        {
            _banService = banService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateBanRequest request)
        {
            var result = await _banService.Create(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result  = await  _banService.GetAll();
            return Ok(result);
        }
		[HttpGet("table")]
		public async Task<IActionResult> GetAll([FromQuery] BaseSearchRequest request)
		{
			var result = await _banService.GetTable(request);
			return Ok(result);
		}
		[HttpDelete("{Id}")]
        public async Task<IActionResult>Delete(long Id)
        {
            var result = await _banService.Delete(Id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBanReuquest reuquest)
        {
            var result = await _banService.Update(reuquest);
            return Ok(result);
        }
    }
}
