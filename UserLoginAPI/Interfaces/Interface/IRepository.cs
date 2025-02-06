using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interface
{
    public interface IRepository<T> where T : class
    {
        public T GetById(int id);
        public T GetByUserName(string userName);
        public IEnumerable<T> GetAll();
        public void Add(T user);
        public void Delete(T user);

    }
}
