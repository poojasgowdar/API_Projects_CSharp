using Azure;
using DataAccess.Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Interfaces
{
    public interface IService
    {
        public IEnumerable<Product> GetAll(string name, int page, int pageSize);
        public Product GetById(int id);
        public ResponseDto Add(ProductCreateDto productdto);
        public ResponseDto AddBulk(List<ProductCreateDto> productDtos);
        public ProductUpdateDto UpdateById(ProductUpdateDto productupdatedto);
        public void DeleteById(int id);
    }
}
