﻿using TheRestaurant.Application.Interfaces;
using TheRestaurant.Domain.Entities.OrderEntities;

namespace TheRestaurant.Application.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public void SetOrderStatus(Order order, string status)
        {
            var orderStatus = new OrderStatus { Status = status };
            order.OrderStatus = orderStatus;
        }

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Implement the logic to create an order (e.g., validation)
            return await _orderRepository.CreateAsync(order);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            // Implement the logic to update an order (e.g., validation)
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            // Implement the logic to delete an order (e.g., validation)
            await _orderRepository.DeleteAsync(orderId);
        }
    }
}
