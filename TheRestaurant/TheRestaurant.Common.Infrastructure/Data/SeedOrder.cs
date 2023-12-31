﻿using Microsoft.EntityFrameworkCore;
using TheRestaurant.Domain.Entities.Orders;

namespace TheRestaurant.Common.Infrastructure.Data
{
    public static class OrderSeed
    {

        public static void SeedOrder(this ModelBuilder modelBuilder)
        {
            int hamburgerProductId = 1;
            int kebabProductId = 2;
            int salmonProductId = 3;
            int caesarProductId = 4;
            int mozzarellaProductId = 5;
            int chocolateProductId = 6;
            int veggocurryProductId = 7;
            int tomyumsoupProductId = 8;
            int lambchopsProductId = 9;
            int blueberrypieProductId = 10;

            var orderStatus = 1;
            var processingStatus = 2;
            var completedStatus = 3;
            var cancelledStatus = 4;
            var activeStatus = 5;


            var order1 = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                OrderStatusId = orderStatus
            };

            var order2 = new Order
            {
                Id = 2,
                OrderDate = DateTime.Now.AddDays(-1),
                OrderStatusId = completedStatus
            };

            var pendingOrders = new List<Order>
            {
                new Order
                {
                    Id = 31,
                    OrderDate = DateTime.Now,
                    OrderStatusId = orderStatus
                },
                new Order
                {
                    Id = 4,
                    OrderDate = DateTime.Now,
                    OrderStatusId = orderStatus
                },
                new Order
                {
                    Id = 5,
                    OrderDate = DateTime.Now,
                    OrderStatusId = orderStatus
                }
            };

            var spreadOrders = new List<Order>
            {
                new Order
                {
                    Id = 34,
                    OrderDate = DateTime.Now.AddDays(-2),
                    OrderStatusId = orderStatus
                },
                new Order
                {
                    Id = 10,
                    OrderDate = DateTime.Now.AddDays(-3),
                    OrderStatusId = completedStatus
                },
                new Order
                {
                    Id = 11,
                    OrderDate = DateTime.Now.AddDays(-4),
                    OrderStatusId = cancelledStatus
                }
            };
            var activeOrders = new List<Order>
            {
                new Order
                {
                    Id = 6,
                    OrderDate = DateTime.Now.AddDays(-1),
                    OrderStatusId = activeStatus
                },
                new Order
                {
                    Id = 32,
                    OrderDate = DateTime.Now.AddDays(-1),
                    OrderStatusId = processingStatus
                },
                new Order
                {
                    Id = 33,
                    OrderDate = DateTime.Now.AddDays(-1),
                    OrderStatusId = activeStatus
                }
            };

            modelBuilder.Entity<Order>().HasData(pendingOrders.ToArray());
            modelBuilder.Entity<Order>().HasData(spreadOrders.ToArray());
            modelBuilder.Entity<Order>().HasData(activeOrders.ToArray());
            modelBuilder.Entity<Order>().HasData(order1, order2);

            var orderRowsForOrder1 = new List<OrderRow>
            {
                new OrderRow { Id = 1, OrderId = 1, ProductId = hamburgerProductId },
                new OrderRow { Id = 2, OrderId = 1, ProductId = kebabProductId },
                new OrderRow { Id = 3, OrderId = 1, ProductId = salmonProductId },
                new OrderRow { Id = 4, OrderId = 1, ProductId = caesarProductId },
                new OrderRow { Id = 5, OrderId = 1, ProductId = mozzarellaProductId }
            };

            var orderRowsForOrder2 = new List<OrderRow>
            {
                new OrderRow { Id = 6, OrderId = 2, ProductId = chocolateProductId },
                new OrderRow { Id = 7, OrderId = 2, ProductId = veggocurryProductId },
                new OrderRow { Id = 8, OrderId = 2, ProductId = tomyumsoupProductId },
                new OrderRow { Id = 9, OrderId = 2, ProductId = lambchopsProductId },
                new OrderRow { Id = 10, OrderId = 2, ProductId = blueberrypieProductId }
            };

            var orderRowsForPendingOrders = new List<OrderRow>
            {
                new OrderRow { Id = 16, OrderId = 31, ProductId = hamburgerProductId },
                new OrderRow { Id = 17, OrderId = 31, ProductId = salmonProductId },
                new OrderRow { Id = 18, OrderId = 4, ProductId = kebabProductId },
                new OrderRow { Id = 19, OrderId = 4, ProductId = mozzarellaProductId },
                new OrderRow { Id = 20, OrderId = 5, ProductId = caesarProductId }
            };

            var orderRowsForActiveOrders = new List<OrderRow>
            {
                new OrderRow { Id = 21, OrderId = 6, ProductId = chocolateProductId },
                new OrderRow { Id = 22, OrderId = 6, ProductId = veggocurryProductId },
                new OrderRow { Id = 23, OrderId = 32, ProductId = tomyumsoupProductId },
                new OrderRow { Id = 24, OrderId = 32, ProductId = lambchopsProductId },
                new OrderRow { Id = 25, OrderId = 33, ProductId = blueberrypieProductId }
            };

            var orderRowsForSpreadOrders = new List<OrderRow>
            {
                new OrderRow { Id = 26, OrderId = 34, ProductId = hamburgerProductId },
                new OrderRow { Id = 27, OrderId = 10, ProductId = salmonProductId },
                new OrderRow { Id = 28, OrderId = 11, ProductId = kebabProductId }
            };

            modelBuilder.Entity<OrderRow>().HasData(orderRowsForPendingOrders.ToArray());
            modelBuilder.Entity<OrderRow>().HasData(orderRowsForActiveOrders.ToArray());
            modelBuilder.Entity<OrderRow>().HasData(orderRowsForSpreadOrders.ToArray());
            modelBuilder.Entity<OrderRow>().HasData(orderRowsForOrder1.ToArray());
            modelBuilder.Entity<OrderRow>().HasData(orderRowsForOrder2.ToArray());
        }
    }
}
