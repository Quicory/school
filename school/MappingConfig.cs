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
            CreateMap<ApplicationUser, UserEditDTO>().ReverseMap();

            CreateMap<Subject, SubjectCreateDTO>().ReverseMap();

            CreateMap<Teacher, TeacherCreateDTO>().ReverseMap();
            CreateMap<Teacher, TeacherUpdateDTO>().ReverseMap();

            CreateMap<Student, StudentCreateDTO>().ReverseMap();
            CreateMap<Student, StudentUpdateDTO>().ReverseMap();

            CreateMap<Classroom, ClassroomCreateDTO>().ReverseMap();
            CreateMap<Classroom, ClassroomUpdateDTO>().ReverseMap();

        }
    }
}
