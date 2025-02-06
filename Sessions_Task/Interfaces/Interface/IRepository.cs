using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interface
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public void Add(T entity);
        public void UpdateById(int id,T entity);
        public void DeleteById(int id);
    }
}
