using AutoMapper;
using HRApplication1.Entity;

namespace HRApplication1.Domain.DTOs
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<CreateDepartmentDTO, Department>();
            CreateMap<ApplicationUser, CreateUserDTO>();
           // CreateMap<CreateRoleDTO, Role>();
           // CreateMap<CreateUserDTO, User>();
            //CreateMap<CreateDepartmentDTO, Department>();
            //CreateMap<GetDepartmentDTO, Department>();

        }

    }
}
