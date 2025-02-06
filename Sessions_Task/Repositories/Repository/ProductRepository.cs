using Interfaces.Interface;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class ProductRepository<T>: IRepository<T> where T :class
    {
        private readonly AppDbContext _context;

        private readonly DbSet<T> _dbSet;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>(); 
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void UpdateById(int id, T entity)
        {
            var existingEntity = _dbSet.Find(id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
        public void DeleteById(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

    }
}
