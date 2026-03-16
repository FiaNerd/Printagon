using Microsoft.AspNetCore.Mvc;
using Printagon.Api.DTOs.Order;
using Printagon.Api.Models;

namespace Printagon.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }
        // GET: api/orders/{orderId}

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);

            if (order == null)
            { 
                return NotFound(); 
            }
            
            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto order)
        {
            var createdOrder = await _orderService.CreateOrderAsync(order);

            return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.Id }, createdOrder);
        }

        // PUT: api/orders/{orderId}

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] OrderUpdateDto order)
        {
            var updaterOrder = await _orderService.UpdateOrderAsync(orderId, order);

            if (updaterOrder == null)
            {
                return NotFound();
            }

            return Ok(updaterOrder);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            var deleteOrder = await _orderService.DeleteOrderAsync(orderId);

            if(deleteOrder == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
