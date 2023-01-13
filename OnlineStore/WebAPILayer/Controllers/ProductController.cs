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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ILogger<ProductController> logger, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Fetching all the Products from the storage");

            var products = await _productService.GetAllAsync().FeedToAsync(_mapper.Map<IEnumerable<ProductViewModel>>);

            _logger.LogInformation("All the Products fetched successful");

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.LogInformation("Fetching the Product from the storage");

            var product = await _productService.GetAsync(id).FeedToAsync(_mapper.Map<ProductViewModel>);

            _logger.LogInformation("The Product fetched successful");

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Adding the Product to the storage");

            var product = await _mapper.Map<Product>(productViewModel).FeedTo(_productService.CreateAsync).FeedToAsync(_mapper.Map<ProductViewModel>);

            _logger.LogInformation("The Product added successful");

            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            _logger.LogInformation("Deleting the Product from the storage");

            await _productService.DeleteByIdAsync(id);

            _logger.LogInformation("The Product deleted successful");

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Updating the Product");

            var product = await _mapper.Map<Product>(productViewModel).FeedTo(_productService.UpdateAsync).FeedToAsync(_mapper.Map<ProductViewModel>);

            _logger.LogInformation("The Product updated successful");

            return Ok(product);
        }
    }
}
