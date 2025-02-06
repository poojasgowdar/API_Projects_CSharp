using Entities.Models;
using Interfaces.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public  class ProductRepository : IRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }
        public void Add(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
        public void UpdateById(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void DeleteById(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        
        }
    }
}
