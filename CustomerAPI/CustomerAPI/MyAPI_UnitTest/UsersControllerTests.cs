using Dtos.dto;
using Entities.Models;
using Interface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductController.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI_UnitTest
{
    public class UsersControllerTests
    {
        private readonly Mock<IService> _mockService;
        private readonly ProducttController _controller;
        public UsersControllerTests()
        {
            _mockService = new Mock<IService>();
            _controller = new ProducttController(_mockService.Object);
        }

        [Fact]
        public void GetProduct_ReturnsOkResultWithProduct()
        {
            // Arrange
            var Id = 2; // Define the user ID to retrieve
            var expectedUser = new Product { Id = 2, Name = "Charger", Price = 6000 };
            _mockService.Setup(service => service.GetById(Id)).Returns(expectedUser); 
            // Act
            var result = _controller.GetById(Id) as OkObjectResult; 
            // Assert
            Assert.NotNull(result); 
            Assert.Equal(200, result.StatusCode); 
            Assert.Equal(expectedUser, result.Value); 
        }
        [Fact]
        public void GetProduct_ReturnsNotFoundWhenProductNotFound()
        {
            // Arrange
            var Id = 99; 
            _mockService.Setup(service => service.GetById(Id)).Returns((Product)null); 
            // Act
            var result = _controller.GetById(Id) as NotFoundResult; 
            // Assert
            Assert.Null(result); 
       
        }
        [Fact]
        public void GetAllProduct_ReturnsOkResultWithListOfProducts()
        {
            // Arrange
            var expectedUsers = new List<Product>
            {

                new Product { Id = 1, Name = "John Doe", Price=600 },
                new Product { Id = 2, Name = "Jane Smith",  Price=600  }
            };
            _mockService.Setup(service => service.GetAll()).Returns(expectedUsers); 
            // Act
            var result = _controller.GetAll() as OkObjectResult; 
            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode); 
            Assert.Equal(expectedUsers, result.Value); 
        }
        [Fact]
        public void AddProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var productDto = new ProductCreateDto { Name = "Sam Wilson", Price = 600 };
            var responseDto = new ResponseDto
            {
                Success = true,
                Message = "Product created successfully."
            };

            _mockService.Setup(service => service.Add(productDto)).Returns(responseDto);

            var _controller = new ProducttController(_mockService.Object);

            // Act
            var result = _controller.Add(productDto) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result); 
            Assert.Equal(201, result.StatusCode);
            Assert.Equal("Product created successfully.", ((ResponseDto)result.Value).Message); 
            Assert.True(((ResponseDto)result.Value).Success);
        }
        [Fact]
        public void UpdateUser_ReturnsNoContent()
        {
            //Arrange
            var productDto = new ProductUpdateDto { Name = "John Updated", Price = 600 };
            var existingProduct = new Product { Id = 1, Name = "John Doe", Price = 500 };

           _mockService.Setup(service => service.GetById(1)).Returns(existingProduct);
           _mockService.Setup(service => service.UpdateById(1, It.IsAny<ProductUpdateDto>())).Verifiable();

            // Act
            var result = _controller.UpdateById(1, productDto); 

            // Assert 
            _mockService.Verify(service => service.UpdateById(1, It.Is<ProductUpdateDto>(p =>
                p.Name == "John Updated" && p.Price == 600
            )), Times.Once);
        
        [Fact]
        public void DeleteProduct_CallsService()
        {
            // Arrange
            var id = 1;
            _mockService.Setup(service => service.DeleteById(id)).Verifiable();
            // Act
            var result = _controller.DeleteById(id);  
            // Assert
            _mockService.Verify(service => service.DeleteById(id), Times.Once); 
        }







    }
}
