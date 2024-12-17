using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.Order;
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
        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {

        }
    }
}
