using DataAccessLayer.Models;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interface
{
    public interface IService
    {
        public string Login(UserLoginDto loginDto);
        public ResponseUserDto Add(CreateUserDto userDto);
        public User GetUserById(int id);
        public void Delete(string user);
        public User GetUserByUsername(string username);
        //public IEnumerable<User> GetAll();
        public IEnumerable<ResponseUserDto> GetAll();
    }
}
