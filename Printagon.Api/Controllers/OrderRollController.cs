using Microsoft.AspNetCore.Mvc;
using Printagon.Api.DTOs.OrderRoll;

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

        // GET: /api/orders/{orderId}/rolls/{rollId}
        // IMPORTANT: Named route so CreatedAtRoute can find it
        [HttpGet("{orderId}/rolls/{rollId}", Name = "GetOrderRollById")]
        public async Task<IActionResult> GetByOrderAndRollAsync(Guid orderId, Guid rollId)
        {
            var orderRoll = await _orderRollService.GetByOrderAndRollAsync(orderId, rollId);

            if (orderRoll == null)
                return NotFound();

            return Ok(orderRoll);
        }

        // POST: /api/orders/{orderId}/order-rolls
        [HttpPost("{orderId}/order-rolls")]
        public async Task<IActionResult> AddAsync(Guid orderId, [FromBody] OrderRollCreateDto dto)
        {
            var created = await _orderRollService.AddAsync(orderId, dto);

            return CreatedAtRoute(
                "GetOrderRollById",
                new { orderId = orderId, rollId = created.RollId },
                created
            );
        }
    }
}
