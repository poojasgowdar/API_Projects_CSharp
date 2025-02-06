using Dtos.Dto;
using Entities.Models;
using Interfaces.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Services.ServiceLayer;


namespace ProductController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService _productService;
        public ProductController(IService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProductById")]
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

        [HttpPost("CreateProduct")]
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

        [HttpPut("UpdateProduct")]
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



        [HttpDelete("DeleteById")]
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
