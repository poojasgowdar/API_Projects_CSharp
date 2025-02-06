using Dtos.Dto;
using Entities.Models;
using Interfaces.Interface;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceLayer
{
    public class ProductService : IService
    {
        private readonly IRepository _productRepository;

        public ProductService(IRepository productRepository)
        {
            _productRepository = productRepository;
        }
       
        public Product GetById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return product;
        }
        public ResponseDto Add(ProductCreateDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                
            };

            _productRepository.Add(product);

            return new ResponseDto
            {
                Message = "Product created successfully.",
                Success = true
            };
        }

       
        public ProductUpdateDto UpdateById(ProductUpdateDto productDto)
        {
            var existingProduct = _productRepository.GetById(productDto.Id);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {productDto.Id} not found.");

            existingProduct.Name = productDto.Name;
            existingProduct.Price = productDto.Price;
            

            _productRepository.UpdateById(existingProduct);
            return productDto;
        }

        public void DeleteById(int id)
        {
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            _productRepository.DeleteById(id);
        }
    }
}
