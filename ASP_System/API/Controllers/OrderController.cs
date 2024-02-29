using Application.Interfaces.Services;
using Domain.Model;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromForm] OrderCreateDTO order)
        {
            try
            {
                var result = await _orderService.CreateOrder(order);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
       
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrderUpdateDTO order)
        {
            var reponse = await _orderService.UpdateOrder(order);
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            return BadRequest(reponse);
        }
        [HttpGet("GetOrder{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await _orderService.GetOrder(id);
            return Ok(result);
        }
        [HttpPut("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(OrderDeleteDTO order)
        {
            var reponse = await _orderService.DeleteOrder(order);
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            return BadRequest(reponse);
        }


    }
}
