using AutoMapper;
using School_Data.DTOs;
using School_Data.Models;

namespace School_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<LoginModel, Token>().ReverseMap();
            CreateMap<ApplicationUser, Token>().ReverseMap();

        }
    }
}
