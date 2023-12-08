﻿using Microsoft.AspNetCore.Mvc;
using TheRestaurant.Application.Orders;
using TheRestaurant.Contracts.Requests.Order;
using TheRestaurant.Presentation.Shared.Requests.Order;
namespace TheRestaurant.Presentation.Server.Controllers.Order
{
    [Route("Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("GetCustomerOrder")]
        public async Task<ActionResult> GetCustomerOrder(int id)
        {
            var result = await _orderService.GetCustomerOrder(id);
            if (! result.IsSuccess)
            {
                return BadRequest();
            }
            
            return Ok(result.Data);
        }


        [HttpPost("CreateOrder")]
        public async Task<ActionResult> CreateOrder([FromBody] PlaceOrderRequest request)
        {
            CreateOrderRequest createOrderRequest = new();

            if (request.Comment != null)
                createOrderRequest.Comment = request.Comment;

            foreach (var item in request.ListOfAggregatedIds)
            {
                ProductAggregateForCreateOrder productAggregate =
                    new(item.Count, item.IdOfOrderAggregate);
                createOrderRequest.ProductAggregate.Add(productAggregate);
            }

            var result = await _orderService.CreateOrderAsync(createOrderRequest);

            if(! result.IsSuccess)
                return BadRequest(result.Data);

            else
                return Ok(result.Data);
        }

        [HttpDelete("CancelOrder/{id}")]
        public async Task<ActionResult> CancelOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet("FetchAllActiveOrders")]
        public async Task<ActionResult> FetchAllActiveOrders()
        {
            var result = await _orderService.GetListOfActiveOrders();
            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok(result.Data);
        }


        [HttpPost("UpdateOrderStatus")]
        public async Task<ActionResult> UpdateOrderStatus([FromBody] int orderId)
        {
            var result = await _orderService.UpdateOrderStatus(orderId);
            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}