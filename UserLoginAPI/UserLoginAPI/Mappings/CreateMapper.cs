using AutoMapper;
using DataAccessLayer.Models;
using Models.Dtos;

namespace UserLoginAPI.Mappings
{
    public class CreateMapper : Profile
    {
        public CreateMapper()
        {
            CreateMap<User, CreateUserDto>();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<ResponseUserDto, CreateUserDto>().ReverseMap();
            CreateMap<User, ResponseUserDto>();
        }
    }

}
