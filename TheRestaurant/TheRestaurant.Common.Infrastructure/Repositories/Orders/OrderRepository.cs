﻿using Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TheRestaurant.Application.Orders.Interfaces;
using TheRestaurant.Domain.Entities.Orders;
using TheRestaurant.Presentation.Shared.DTO.Dashboard;

namespace TheRestaurant.Common.Infrastructure.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(
            RestaurantDbContext dbContext,
            ILogger<OrderRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<bool> CreateOrderRow(OrderRow orderRow)
        {
            _dbContext.OrderRows.Add(orderRow);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            return await _dbContext.Orders
                .Include(x => x.Employee)
                .Include(x => x.OrderRows)
                    .ThenInclude(o => o.Product)
                    .ThenInclude(v => v.VAT)
                .Include(x => x.OrderStatus)
                .Where(x => x.Id == orderId && x.IsDeleted != true).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            _dbContext.Orders.Update(order);

            try
            {
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }

        public async Task DeleteAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync(); 
        }


        public async Task<List<Order>> GetPendingOrders()
        {
            return await _dbContext.Orders
                          .Include(x => x.OrderRows)
                              .ThenInclude(o => o.Product)
                          .Include(x => x.OrderStatus)
                          .Include(x => x.Employee)
                          .Where(x => x.OrderStatus.Status == "Pending" && x.IsDeleted != true)
                          .ToListAsync();
        }


        public async Task<List<Order>> GetActiveOrders()
        {
            return await _dbContext.Orders
                           .Include(x => x.OrderRows)
                               .ThenInclude(o => o.Product)
                           .Include(x => x.OrderStatus)
                           .Include(x => x.Employee)
                           .Where(x => x.OrderStatus.Status == "Processing" && x.IsDeleted != true)
                           .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByStatus(string status)
        {
            var orders = await _dbContext.Orders.Where(x => x.OrderStatus.Status == status).ToListAsync();
            return orders;

        }

        public async Task<List<Order>> GetFinishedOrders()
        {
            return await _dbContext.Orders
                          .Include(x => x.OrderRows)
                            .ThenInclude(o => o.Product)
                          .Include(x => x.Employee)
                          .Where(x => x.OrderStatus.Status == "Delivered" && x.IsDeleted != true)
                          .ToListAsync();
        }

        //public async Task<OrderStatus> GetOrderStatusByName(string statusName)
        //{
        //    return await _dbContext.OrderStatus
        //                            .Where(x => x.Status == statusName)
        //                            .SingleOrDefaultAsync();
        //}

        public async Task<List<ProductSaleCountDto>> GetProductSaleCount()
        {
            var productSaleCounts = await _dbContext.OrderRows
                .Where(or => or.ProductId != 0)
                .Include(or => or.Product) // Ensure Product data is included
                .GroupBy(or => or.Product.Name)
                .Select(group => new ProductSaleCountDto(group.Key, group.Count()))
                .ToListAsync();

            return productSaleCounts;
        }
        public async Task<List<OrderCountByHourDto>> GetOrderStatsByDatePicked(DateTime selectedDate)
        {
            return await _dbContext.Orders
                .Where(order => order.OrderDate.Date == selectedDate.Date)
                .GroupBy(order => order.OrderDate.Hour)
                .Select(group => new OrderCountByHourDto(group.Key, group.Count()))
                .ToListAsync();
        }
        public async Task<List<OrderCountDto>> GetOrderStatsByDateRange(DateTime orderStartDate, DateTime orderEndDate)
        {
            return await _dbContext.Orders
                .Where(order => order.OrderDate.Date >= orderStartDate.Date && order.OrderDate.Date <= orderEndDate.Date)
                .GroupBy(order => order.OrderDate.Date)
                .Select(group => new OrderCountDto(group.Key.ToString("yyyy-MM-dd"), group.Count(), group.Key))
                .ToListAsync();
        }
    }
}
