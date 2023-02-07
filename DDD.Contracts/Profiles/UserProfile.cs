using AutoMapper;
using DDD.Contracts.DTO.Authenticate;
using DDD.Contracts.DTO.User;
using DDD.Domain.Identity;

namespace DDD.Contracts.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDTO>()
             .ForMember(
                dest => dest.UserName,
                from => from.MapFrom(x => x.UserName))
             .ForMember(
                dest => dest.FirstName,
                from => from.MapFrom(x => x.FirstName))
            .ForMember(
                dest => dest.LastName,
                from => from.MapFrom(x => x.LastName))
            .ForMember(
                dest => dest.Email,
                from => from.MapFrom(x => x.Email))
            .ForMember(
                dest => dest.PhoneNumber,
                from => from.MapFrom(x => x.PhoneNumber))
            .ForMember(
                dest => dest.Country,
                from => from.MapFrom(x => x.Country))
            .ForMember(
                dest => dest.DateOfBirth,
                from => from.MapFrom(x => x.DateOfBirth))
            .ForMember(
                dest => dest.Role,
                from => from.MapFrom(x => x.Role));

            CreateMap<CreateUserDTO, ApplicationUser>()
             .ForMember(
                dest => dest.UserName,
                from => from.MapFrom(x => x.UserName))
             .ForMember(
                dest => dest.FirstName,
                from => from.MapFrom(x => x.FirstName))
            .ForMember(
                dest => dest.LastName,
                from => from.MapFrom(x => x.LastName))
            .ForMember(
                dest => dest.Email,
                from => from.MapFrom(x => x.Email))
            .ForMember(
                dest => dest.PhoneNumber,
                from => from.MapFrom(x => x.PhoneNumber))
            .ForMember(
                dest => dest.Country,
                from => from.MapFrom(x => x.Country))
            .ForMember(
                dest => dest.DateOfBirth,
                from => from.MapFrom(x => x.DateOfBirth))
            .ForMember(
                dest => dest.Role,
                from => from.MapFrom(x => x.Role));

            CreateMap<UpdateUserDTO, ApplicationUser>()
                .ForMember(
                dest => dest.UserName,
                from => from.MapFrom(x => x.UserName))
             .ForMember(
                dest => dest.FirstName,
                from => from.MapFrom(x => x.FirstName))
            .ForMember(
                dest => dest.LastName,
                from => from.MapFrom(x => x.LastName))
            .ForMember(
                dest => dest.Email,
                from => from.MapFrom(x => x.Email))
            .ForMember(
                dest => dest.PhoneNumber,
                from => from.MapFrom(x => x.PhoneNumber))
            .ForMember(
                dest => dest.Country,
                from => from.MapFrom(x => x.Country))
            .ForMember(
                dest => dest.DateOfBirth,
                from => from.MapFrom(x => x.DateOfBirth))
            .ForMember(
                dest => dest.Role,
                from => from.MapFrom(x => x.Role));

            // User -> AuthenticateResponse
            CreateMap<ApplicationUser, UserLoginRequestDTO>();

            // RegisterRequest -> User
            CreateMap<UserRegistrationRequestDTO, ApplicationUser>();
        }
    }
}