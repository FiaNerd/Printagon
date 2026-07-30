using Microsoft.AspNetCore.Mvc;

namespace Printagon.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderRollController : ControllerBase
    {
        private readonly IOrderRollService _orderRollService;

        public OrderRollController(IOrderRollService orderRollService)
        {
            _orderRollService = orderRollService;
        }

        // GET: /api/orders/{orderId}/order-rolls
        [HttpGet("{orderId}/order-rolls")]
        public async Task<IActionResult> GetAllByOrderIdAsync(Guid orderId)
        {
            var orderRolls = await _orderRollService.GetAllByOrderIdAsync(orderId);
            return Ok(orderRolls);
        }

        // GET: /api/orders/{orderId}/rolls/{rollId} - order and rolls based on id
        [HttpGet("{orderId}/rolls/{rollId}")]
        public async Task<IActionResult> GetByOrderAndRollAsync(Guid orderId, Guid rollId)
        {
            var orderRoll = _orderRollService.GetByOrderAndRollAsync(orderId, rollId);

            return Ok(orderRoll);
        }
    }
}
