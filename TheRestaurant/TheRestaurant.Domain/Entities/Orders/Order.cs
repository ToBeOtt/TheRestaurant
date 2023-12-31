﻿using TheRestaurant.Domain.Entities.Authentication;

namespace TheRestaurant.Domain.Entities.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } 

        public string? OrderComment { get; set; }

        public string? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public bool IsDeleted { get; set; } = false;

        public OrderStatus OrderStatus { get; set; }

        public int OrderStatusId { get; set; }
        public ICollection<OrderRow>? OrderRows { get; set; }

        public Order()
        {
            
        }
    }
}
