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

        // GET: /api/orders/{orderNumber}/order-rolls
        [HttpGet("{orderNumber}/order-rolls")]
        public async Task<IActionResult> GetAllByOrderNumberAsync(int orderNumber)
        {
            var orderRolls = await _orderRollService.GetAllByOrderNumberAsync(orderNumber);
            return Ok(orderRolls);
        }

        // GET: /api/orders/{orderNumber}/rolls/{rollNumber}
        [HttpGet("{orderNumber}/rolls/{rollNumber}", Name = "GetOrderRollById")]
        public async Task<IActionResult> GetByOrderAndRollAsync(int orderNumber, int rollNumber)
        {
            var orderRoll = await _orderRollService.GetByOrderAndRollAsync(orderNumber, rollNumber);

            if (orderRoll == null)
            {
                return NotFound();
            }

            return Ok(orderRoll);
        }

        // POST: /api/orders/{orderNumber}/order-rolls
        [HttpPost("{orderNumber}/order-rolls")]
        public async Task<IActionResult> AddAsync(int orderNumber, [FromBody] OrderRollCreateDto dto)
        {
            var created = await _orderRollService.AddAsync(orderNumber, dto);

            return CreatedAtRoute(
                "GetOrderRollById",
                new { orderNumber = orderNumber, rollNumber = created.RollNumber },
                created
            );
        }

        [HttpPut("/api/order-rolls/{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] OrderRollUpdateDto dto)
        {
            var updated = await _orderRollService.UpdateAsync(id, dto);

            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("/api/order-rolls/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleted = await _orderRollService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
