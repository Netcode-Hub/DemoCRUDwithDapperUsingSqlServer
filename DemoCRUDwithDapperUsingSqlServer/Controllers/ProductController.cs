using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DemoCRUDwithDapperUsingSqlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProduct _productRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            int result = await _productRepository.AddProductAsync(product);
            if (result == 0)
                return BadRequest();
            else
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            int result = await _productRepository.UpdateProductAsync(product);
            return result > 0 ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int result = await _productRepository.DeleteProductAsync(id);
            return result > 0 ? NoContent() : BadRequest();
        }
    }
}
