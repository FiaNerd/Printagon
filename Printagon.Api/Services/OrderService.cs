using Printagon.Api.DTOs.Order;
using Printagon.Api.Repositories.Interfaces;

namespace Printagon.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if (order == null)
            { 
                return null;
            }    
            
            var result = new OrderResponseDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                JobName = order.JobName,
                YearlyNumber = order.YearlyNumber,
                PaperType = order.PaperType,
                GramWeight = order.GramWeight,
                RollWidth = order.RollWidth,
                OrderStatus = order.OrderStatus,
                Comment = order.Comment,
                CreatedAt = order.CreatedAt
            };
            return result;
        }


        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var order = await _orderRepository.GetAllOrdersAsync();

            var result = order.Select(o => new OrderResponseDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                JobName = o.JobName,
                YearlyNumber = o.YearlyNumber,
                PaperType = o.PaperType,
                GramWeight = o.GramWeight,
                RollWidth = o.RollWidth,
                OrderStatus = o.OrderStatus,
                Comment = o.Comment,
                CreatedAt = o.CreatedAt
            });

            return result;
        }


        public Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponseDto?> UpdateOrderAsync(Guid orderId, OrderUpdateDto orderUpdateDto)
        {
            throw new NotImplementedException();
        }


        public Task<bool> DeleteOrderAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
