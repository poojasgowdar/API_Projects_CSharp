using Dtos.Dto;
using Entities.Models;
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
        public ResponseDto Add(ProductCreateDto productdto);

        public ProductUpdateDto UpdateById(ProductUpdateDto productupdatedto);
        public void DeleteById(int id);
    }
}





       