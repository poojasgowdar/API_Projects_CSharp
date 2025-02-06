using Interface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;


namespace Mangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Productcontroller : ControllerBase
    {
        private readonly IService _productService;

        public Productcontroller(IService productService)
        {
            _productService = productService;
        }

        // 1. GET /api/products
        [HttpGet]
        public IActionResult GetAll([FromQuery] string name, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = _productService.GetAll(name, page, pageSize);
            return Ok(products);
        }


        // 2.GET: api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _productService.GetById(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        // 3.POST: api/products
        [HttpPost("CreateSingleProduct")]
        public IActionResult Add([FromBody] ProductCreateDto productDto)
        {
            try
            {
                var response = _productService.Add(productDto);
                return CreatedAtAction(nameof(GetById), new { id = response.Success }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 4.POST: api/products
        [HttpPost("CreateBulkProduct")]
        public IActionResult AddBulk([FromBody] List<ProductCreateDto> productDtos)
        {
            try
            {
                var response = _productService.AddBulk(productDtos);
                return Created("", response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // 5.PUT: api/products/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductUpdateDto productDto)
        {
            if (id != productDto.Id)
                return BadRequest("Product ID mismatch.");

            try
            {
                var updatedProduct = _productService.UpdateById(productDto);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 6. DELETE /api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.DeleteById(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
