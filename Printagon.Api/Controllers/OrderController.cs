using Microsoft.AspNetCore.Mvc;
using Printagon.Api.DTOs.Order;

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
        // GET: api/orders/{OrderNumber}

        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> GetOrderById(int orderNumber)
        {
            var order = await _orderService.GetOrderByIdAsync(orderNumber);

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

            return CreatedAtAction(nameof(GetOrderById), new { OrderNumber = createdOrder.Id }, createdOrder);
        }

        // PUT: api/orders/{OrderNumber}

        [HttpPut("{orderNumber}")]
        public async Task<IActionResult> UpdateOrder(int orderNumber, [FromBody] OrderUpdateDto order)
        {
            var updaterOrder = await _orderService.UpdateOrderAsync(orderNumber, order);

            if (updaterOrder == null)
            {
                return NotFound();
            }

            return Ok(updaterOrder);
        }

        [HttpDelete("{orderNumber}")]
        public async Task<IActionResult> DeleteOrder(int orderNumber)
        {
            var deleteOrder = await _orderService.DeleteOrderAsync(orderNumber);

            if(deleteOrder == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
