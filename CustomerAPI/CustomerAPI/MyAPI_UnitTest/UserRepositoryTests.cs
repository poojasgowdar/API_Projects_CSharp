using Dtos.dto;
using Entities.Models;
using Interface.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI_UnitTest
{
    public class UserRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly ProductRepository _userRepository;
      
        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new AppDbContext(options);

            _userRepository = new ProductRepository(_context);
        }
       
        [Fact]
        public void GetUserById_ReturnsCorrectProduct()
        {
            // Arrange
            var product = new Product { Id = 2, Name = "Test Product", Price = 100 };
            _context.Products.Add(product);
            _context.SaveChanges(); 

            // Act
            var result = _userRepository.GetById(2);

            // Assert
            Assert.NotNull(result); // Verify the result is not null
            Assert.Equal(2, result.Id); // Verify the ID matches
        }
        [Fact]
        public void GetProductById_ReturnsNullWhenProductNotFound()
        {
            // Arrange
            var Id = 99;
            // Act
            var result = _userRepository.GetById(Id);
            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void GetAllProducts_ReturnsAllProducts()
        {
            // Act
            var result = _userRepository.GetAll();
            // Assert
         
            Assert.NotNull(result);
       
            Assert.Equal(0, result.Count());
        }
        [Fact]
        public void AddProduct_AddProductCorrectly()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "John Doe", Price = 600 };
            // Act
            _context.Add(product);
            var result = _userRepository.GetById(1);
            // Assert
            Assert.NotNull(result); 
        }
        [Fact]
        public void UpdateProduct_UpdatesProductCorrectly()
        {
            // Arrange
            _context.Products.Add(new Product { Id = 1, Name = "John Doe", Price = 500 });
            _context.SaveChanges();

            var existingProduct = _context.Products.Find(1); 
            existingProduct.Name = "John Updated";
            existingProduct.Price = 600;

            // Act
            _userRepository.UpdateById(1, existingProduct);
            var result = _userRepository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Updated", result.Name);
            Assert.Equal(600, result.Price);
        }

        [Fact]
        public void DeleteProduct_DeleteProductCorrectly()
        {
            //Arrange
            var id = 1;
            //Act
            _userRepository.DeleteById(id);
             var result = _userRepository.GetById(id);
            //Action
             Assert.Null(result);
        }


    }
}
