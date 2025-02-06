using Dtos.dto;
using Entities.Models;
using Interface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ProductController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducttController : ControllerBase
    {
        private readonly IService _productService;

        public ProducttController(IService productService)
        {
            _productService = productService;
        }

       
        [HttpGet("GetAllProducts")]
        public IActionResult GetAll()
        {
            try
            {
                var products = _productService.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetProductById{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _productService.GetById(id);
                if (product == null)
                {
                    return NotFound(new { message = $"Product with ID {id} not found." });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("CreateNewProduct")]
        public IActionResult Add([FromBody] ProductCreateDto productDto)
        {
           try
            {
                var response = _productService.Add(productDto);
                if (response.Success)
                {
                    return CreatedAtAction(nameof(GetById), new { id = response.Message }, response);
                }
                return BadRequest(new { message = response.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("UpdateProductById/{id}")]
        public IActionResult UpdateById(int id, [FromBody] ProductUpdateDto productDto)
        {
            try
            {
                var response = _productService.UpdateById(id, productDto);
                if (response.Success)
                {
                    return Ok(response);
                }
                return NotFound(new { message = response.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

     
        [HttpDelete("DeleteProductById/{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                var response = _productService.DeleteById(id);
                if(response.Success)
                {
                   return Ok(response);
                }
                return NotFound(new { message = response.Message });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
