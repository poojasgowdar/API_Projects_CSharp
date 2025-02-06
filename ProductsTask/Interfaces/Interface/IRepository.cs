using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interface
{
    public interface IRepository
    {
        public Product GetById(int id);
        public void Add(Product Product);
        public void UpdateById(Product product);
        public void DeleteById(int id); 

    }
}
