using AutoMapper;
using BLL.Models;
using BLL.Services.Interfaces;
using FunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPILayer.Models;

namespace WebAPILayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Fetching all the Orders from the storage");

            var orders = await _orderService.GetAllAsync().FeedToAsync(_mapper.Map<IEnumerable<OrderViewModel>>);

            _logger.LogInformation("All the Orders fetched successful");

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.LogInformation("Fetching the Order from the storage");

            var order = await _orderService.GetAsync(id).FeedToAsync(_mapper.Map<OrderViewModel>);

            _logger.LogInformation("The Order fetched successful");

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Adding the Order to the storage");

            var order = await _mapper.Map<Order>(orderViewModel).FeedTo(_orderService.CreateAsync).FeedToAsync(_mapper.Map<OrderViewModel>);

            _logger.LogInformation("The Order added successful");

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            _logger.LogInformation("Deleting the Order from the storage");

            await _orderService.DeleteByIdAsync(id);

            _logger.LogInformation("The Order deleted successful");

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Updating the Order");

            var order = await _mapper.Map<Order>(orderViewModel).FeedTo(_orderService.UpdateAsync).FeedToAsync(_mapper.Map<OrderViewModel>);

            _logger.LogInformation("The Order updated successful");

            return Ok(order);
        }
    }
}
