using Azure;
using Dtos.Dto;
using Interfaces.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SessionTracking_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IService _productService;

        public SessionController(IService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAll()
        {
            try
            {
                var products = _productService.GetAll();
                if (products == null || !products.Any())
                {
                    return NotFound(new { message = "No products found." });
                }
                int? viewedProductCount = HttpContext.Session.GetInt32("ViewedProductCount");
                if (viewedProductCount == null)
                {
                    viewedProductCount = 0;
                }
                viewedProductCount += products.Count();
                HttpContext.Session.SetInt32("ViewedProductCount", viewedProductCount.Value);
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(1),
                    HttpOnly = true
                };
                Response.Cookies.Append("ProductCount", products.Count().ToString(), cookieOptions);
                return Ok(new
                {
                    message = "Products retrieved successfully.",
                    products = products,
                    viewedProductCount = viewedProductCount
                });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("GetProductById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var response = _productService.GetById(id);

                if (response == null)
                {
                    return NotFound(new { message = $"Product with ID {id} not found." });
                }
              
                int? lastViewedProductId = HttpContext.Session.GetInt32("LastViewedProductId");
                string lastViewedProductName = HttpContext.Session.GetString("LastViewedProductName");
                HttpContext.Session.SetInt32("LastViewedProductId", id);
                HttpContext.Session.SetString("LastViewedProductName", response.Name);
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(1),
                    HttpOnly = true
                };
                Response.Cookies.Append("ProductStatus", $"Product with ID {id} Found", cookieOptions);
                return Ok(new
                {
                    message = "Product retrieved successfully.",
                    product = response,
                    lastViewed = new
                    {
                        lastViewedProductId,
                        lastViewedProductName
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }


        [HttpPost("CreateNewProduct")]
        public IActionResult Add([FromBody] ProductCreateDto productDto)
        {
            try
            {
                var response = _productService.Add(productDto);
                HttpContext.Session.SetString("LastCreatedProductName", productDto.Name);
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(1),
                    HttpOnly = true
                };
                Response.Cookies.Append("ProductCreated", productDto.Name, cookieOptions);
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
                    HttpContext.Session.SetString("LastUpdatedProductName", productDto.Name);
                    HttpContext.Session.SetString("LastUpdatedProductPrice", productDto.Price.ToString());
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(1),
                        HttpOnly = true
                    };
                    Response.Cookies.Append("ProductUpdated_Name", productDto.Name, cookieOptions);
                    Response.Cookies.Append("ProductUpdated_Price", productDto.Price.ToString(), cookieOptions);
                    return Ok(response);
                }
                return NotFound(new { message = response.Message });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpDelete("DeleteProductById/{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                var response = _productService.DeleteById(id);
                if (response.Success)
                {
                    HttpContext.Session.SetInt32("LastDeletedProductId", id);
                    HttpContext.Session.SetString("LastDeletedProductMessage", $"Product with ID {id} deleted successfully");
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(1),
                        HttpOnly = true
                    };
                    Response.Cookies.Append("ProductDeleted", $"Product with ID {id} deleted successfully", cookieOptions);
                    return Ok(new { message = "Product deleted successfully." });
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
