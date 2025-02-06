using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Interfaces
{
    public interface IRepository
    {
        public Product GetById(int id);
        public IEnumerable<Product> GetAll();
        public void Add(Product product);
        public void UpdateById(int id, Product product);
        public void DeleteById(int id);
    }
}

