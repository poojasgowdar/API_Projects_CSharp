using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Interfaces
{
    public interface IRepository<Product> where Product : class
    {
        public IEnumerable<Product> GetAll(string name, int page, int pageSize);
        public Product GetById(int id);
        public void Add(Product product);
        public void AddBulk(IEnumerable<Product> entities);
        public void UpdateById(Product product);
        public void DeleteById(int id);
    }
}
