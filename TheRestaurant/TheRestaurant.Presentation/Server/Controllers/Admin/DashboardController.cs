﻿using Microsoft.AspNetCore.Mvc;
using TheRestaurant.Application.Dashboard;
using TheRestaurant.Application.Orders;
using TheRestaurant.Contracts.Responses.ServiceResponse;

namespace TheRestaurant.Presentation.Server.Controllers.Admin
{
    [ApiController]
    [Route("Dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
		private readonly DashboardServices _dashboardServices;
        private readonly OrderService _orderService;

        public DashboardController(
            ILogger<DashboardController> logger,
			DashboardServices dashboardServices,
            OrderService orderService)
        {
            _logger = logger;
			_dashboardServices = dashboardServices;
            _orderService = orderService;
		}


        [HttpGet("GetWeeklyOrderStatistics")]
        public async Task<IActionResult> GetWeeklyOrderStatistics([FromQuery] DateTime? date)
        {
            // If no date is provided, use the current date
            DateTime fromDate = date ?? DateTime.Now;

            var result = await _dashboardServices.GetWeeklyOrderStats(fromDate);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorResponse);
            }

            return Ok(result.Data);
        }

        [HttpGet("GetOrderStatsByUserChosenDate")]
        public async Task<ActionResult<ServiceResponse<List<OrderCountByHourDto>>>> GetOrderStatsByUserChosenDate([FromQuery] DateTime selectedDate)
        {
            System.Diagnostics.Debugger.Break();

            var response = await _orderService.GetOrderStatsByUserChosenDate(selectedDate);
            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }
            return Ok(response);
        }

        [HttpGet("GetProductSaleCount")]
        public async Task<ActionResult<ServiceResponse<List<ProductSaleCountDto>>>> GetProductSaleCount()
        {
            var response = await _orderService.GetProductSaleCounts();
            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }
            return Ok(response);
        }
    }
}
