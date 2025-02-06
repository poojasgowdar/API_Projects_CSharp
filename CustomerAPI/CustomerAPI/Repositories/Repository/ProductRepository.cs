using Entities.Models;
using Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class ProductRepository:IRepository
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
        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void UpdateById(int id, Product product)
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                _context.SaveChanges();
            }
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


