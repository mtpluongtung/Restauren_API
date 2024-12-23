using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.Bep;
using Models.DTO.Request.Order;
using Models.Entities;
using Repositories.Interfaces;

namespace Restaurant_API.Controllers
{
    [Route("Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderSevices _orderSevices;
        public OrderController(IOrderSevices orderSevices)
        {
            _orderSevices = orderSevices;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrder order)
        {
            var result = await _orderSevices.Create(order);
            return Ok(result);
        }
        [HttpGet("/menu")]
        public async Task<IActionResult> GetMenu()
        {
            var result = await _orderSevices.GetMenu();
            return Ok(result);
        }
        [HttpPost("/them-mon")]
        public async Task<IActionResult> ThemMon(BepCreateRequest request)
        {
            return Ok(request);
        }
        [HttpGet("{banId}")]
        public async Task<IActionResult> GetOrder(long banId)
		{
			var result = await _orderSevices.GetByBanId(banId);
			return Ok(result);
		}
		[HttpPost("goi-mon")]
		public async Task<IActionResult> GoiMon(BepCreateRequest request)
		{
            var result = await _orderSevices.KhachHangThemMon(request);
            return Ok(result);
		}
	}
}
