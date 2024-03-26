using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTwebAPI.Models;
using RESTwebAPI.Services;

namespace RESTwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var order = await _orderService.GetAllOrdersAsync();
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order.Data == null)
            {
                return NotFound();
            }
            return Ok(order.Data);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            var newOrder = await _orderService.AddOrderAsync(order);
            return CreatedAtAction(nameof(Get), new { id = newOrder.Data.OrderId }, newOrder.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var updatedOrder = await _orderService.UpdateOrderAsync(id, order);
            if (updatedOrder.Data == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedOrder = await _orderService.DeleteOrderAsync(id);
            if (deletedOrder.Data == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
