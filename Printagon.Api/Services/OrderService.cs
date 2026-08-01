using Printagon.Api.DTOs.Order;
using Printagon.Api.Models;
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

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int orderNumber)
        {
            var order = await _orderRepository.GetOrderByNumberAsync(orderNumber);

            if (order == null)
            { 
                return null;
            }    
            
            var result = new OrderResponseDto
            {
                OrderNumber = order.OrderNumber,
                JobName = order.JobName,
                YearlyNumber = order.YearlyNumber,
                PaperType = order.PaperType,
                PaperGramWeight = order.PaperGramWeight,
                PaperWidth = order.PaperWidth,
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
                OrderNumber = o.OrderNumber,
                JobName = o.JobName,
                YearlyNumber = o.YearlyNumber,
                PaperType = o.PaperType,
                PaperGramWeight = o.PaperGramWeight,
                PaperWidth = o.PaperWidth,
                OrderStatus = o.OrderStatus,
                Comment = o.Comment,
                CreatedAt = o.CreatedAt
            });

            return result;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
           var order  = new Order
           {
                OrderNumber = orderCreateDto.OrderNumber,
                JobName = orderCreateDto.JobName,
                YearlyNumber = orderCreateDto.YearlyNumber,
                PaperType = orderCreateDto.PaperType,
                PaperGramWeight = orderCreateDto.PaperGramWeight,
                PaperWidth = orderCreateDto.PaperWidth,
                OrderStatus = orderCreateDto.OrderStatus,
                Comment = orderCreateDto.Comment,
                CreatedAt = DateTime.UtcNow
           };

            var createdOrder = await _orderRepository.CreateOrderByNumberAsync(order);

            var result = new OrderResponseDto
            {
                OrderNumber = createdOrder.OrderNumber,
                JobName = createdOrder.JobName,
                YearlyNumber = createdOrder.YearlyNumber,
                PaperType = createdOrder.PaperType,
                PaperGramWeight = createdOrder.PaperGramWeight,
                PaperWidth = createdOrder.PaperWidth,
                OrderStatus = createdOrder.OrderStatus,
                Comment = createdOrder.Comment,
                CreatedAt = createdOrder.CreatedAt
            };

            return result;
        }

        public async Task<OrderResponseDto?> UpdateOrderAsync(int OrderNumber, OrderUpdateDto orderUpdateDto)
        {
            var existingOrder = await _orderRepository.GetOrderByNumberAsync(OrderNumber);

            if(existingOrder == null)
            {
                return null;
            }
            existingOrder.OrderNumber = orderUpdateDto.OrderNumber;
            existingOrder.JobName = orderUpdateDto.JobName;
            existingOrder.YearlyNumber = orderUpdateDto.YearlyNumber;
            existingOrder.PaperType = orderUpdateDto.PaperType;
            existingOrder.PaperGramWeight = orderUpdateDto.PaperGramWeight;
            existingOrder.PaperWidth = orderUpdateDto.PaperWidth;
            existingOrder.OrderStatus = orderUpdateDto.OrderStatus;
            existingOrder.Comment = orderUpdateDto.Comment;

            var updatedOrder = await _orderRepository.UpdateOrderAsync(OrderNumber, existingOrder);

            var result = new OrderResponseDto
            {
                OrderNumber = updatedOrder.OrderNumber,
                JobName = updatedOrder.JobName,
                YearlyNumber = updatedOrder.YearlyNumber,
                PaperType = updatedOrder.PaperType,
                PaperGramWeight = updatedOrder.PaperGramWeight,
                PaperWidth = updatedOrder.PaperWidth,
                OrderStatus = updatedOrder.OrderStatus,
                Comment = updatedOrder.Comment,
                CreatedAt = updatedOrder.CreatedAt
            };

            return result;
        }


        public async Task<bool> DeleteOrderAsync(int orderNumber)
        {
            var deleteOrder = await _orderRepository.DeleteOrderAsync(orderNumber);

            return deleteOrder;
        }
    }
}
