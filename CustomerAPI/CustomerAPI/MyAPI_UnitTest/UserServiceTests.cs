using Dtos.dto;
using Entities.Models;
using Interface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using ProductController.Controllers;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI_UnitTest
{
    public class UserServiceTests
    {
        //private readonly ProducttController _controller;
        private readonly Mock<IRepository> _mockRepository;
        private readonly ProductService _service;
        public UserServiceTests()
        {
            _mockRepository = new Mock<IRepository>();
            _service = new ProductService(_mockRepository.Object);
            //_controller= new ProducttController(service);
        }
        [Fact]
        public void GetUserById_ReturnsProduct()
        {
            // Arrange
            var Id = 2;
            var product = new Product { Id = 2, Name = "Charger", Price = 6000};
            _mockRepository.Setup(repo => repo.GetById(Id)).Returns(product);
            // Act
            var result = _service.GetById(Id);
            
            // Assert
            Assert.NotNull(result); 
            
            Assert.Equal(product, result);
            _mockRepository.Verify(repo => repo.GetById(Id),Times.Once());


        }
        [Fact]
        public void GetUserById_ReturnsNullWhenProductNotFound()
        {
            // Arrange
            var Id = 99;
            _mockRepository.Setup(repo => repo.GetById(Id)).Returns((Product)null);
            // Act
            var result = _service.GetById(Id);
            // Assert
            Assert.Null(result);
           
        }
        [Fact]
        public void GetAllProducts_ReturnsListOfProducts()
        {
            // Arrange
            var expectedUsers = new List<Product>
            {
                new Product { Id = 1, Name = "John Doe", Price=600 },
                new Product { Id = 2, Name = "Jane Smith",  Price=600  }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(expectedUsers);
            // Act
            var result = _service.GetAll();
            // Assert
            Assert.NotNull(result); 
        }
        [Fact]
        public void AddProduct_CallsRepository()
        {
            // Arrange
            var productDto = new ProductCreateDto { Name = "John Doe", Price = 600 };
            // Act
            _service.Add(productDto);
            // Assert
           _mockRepository.Verify(repo => repo.Add(It.Is<Product>(p => p.Name == "John Doe" && p.Price == 600)), Times.Once);
        }
        [Fact]
        public void UpdateProduct_CallsRepository()
        {
            // Arrange
            var productDto = new ProductUpdateDto { Name = "John Updated", Price = 600 };
            var existingProduct = new Product { Id = 1, Name = "John Doe", Price = 500 };

          _mockRepository
                .Setup(repo => repo.GetById(1))
                .Returns(existingProduct);

          _mockRepository
                .Setup(repo => repo.UpdateById(1, It.IsAny<Product>()))
                .Verifiable(); 

            // Act
            _service.UpdateById(1, productDto);
            _mockRepository.Verify(repo => repo.UpdateById(1, It.Is<Product>(p =>
                p.Name == "John Updated" && p.Price == 600
            )), Times.Once);
        }
        [Fact]
        public void DeleteProduct_CallsRepository()
        {
            // Arrange
            var id = 1;
            _mockRepository.Setup(repo => repo.GetById(id)).Returns(new Product { Id = id, Name = "Product" });
            _mockRepository.Setup(repo => repo.DeleteById(id)).Verifiable();

            // Act
            _service.DeleteById(id);  

            // Assert
            _mockRepository.Verify(repo => repo.DeleteById(id), Times.Once); 
        }
    }


}
