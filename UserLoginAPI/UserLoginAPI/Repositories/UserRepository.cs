using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Interfaces.Interface;
using Microsoft.EntityFrameworkCore;

namespace UserLoginAPI.Repositories
{
    public class UserRepository<T> : IRepository<User>
    {
        private readonly UsersDbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(UsersDbContext context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public void Add(User user)
        {
            _dbSet.Add(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _dbSet.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _dbSet.ToList();
        }

        public User GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public User GetByUserName(string userName)
        {
            return _dbSet.FirstOrDefault(u => u.UserName == userName);
        }
        //public void SaveChanges()
        //{
        //    _context.SaveChanges();
        //}
    }
}
