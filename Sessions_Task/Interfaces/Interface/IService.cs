using Dtos.Dto;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interface
{
    public interface IService
    {
        public Product GetById(int id);
        public IEnumerable<Product> GetAll();
        public ResponseDto Add(ProductCreateDto product);
        public ResponseDto UpdateById(int id, ProductUpdateDto product);
        public ResponseDto DeleteById(int id);
    }
}
