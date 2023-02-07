using AutoMapper;
using DDD.Contracts.DTO.Authenticate;
using DDD.Contracts.DTO.Role;
using DDD.Contracts.DTO.User;
using DDD.Domain.Identity;

namespace DDD.Contracts.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<ApplicationRole, RoleDTO>()             
            .ForMember(
                dest => dest.Role,
                from => from.MapFrom(x => x.Name));

            CreateMap<CreateRoleDTO, ApplicationRole>()                     
            .ForMember(
                dest => dest.Name,
                from => from.MapFrom(x => x.Role));

            CreateMap<UpdateRoleDTO, ApplicationRole>()               
            .ForMember(
                dest => dest.Name,
                from => from.MapFrom(x => x.Role));
        }
    }
}