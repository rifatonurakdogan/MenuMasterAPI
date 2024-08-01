using AutoMapper;
using MenuMasterAPI.Application.DTOs;
using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Application.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserContract>();
        CreateMap<User, UserContract>().ReverseMap();

        CreateMap<User, UserUpdateContract>();
        CreateMap<User, UserUpdateContract>().ReverseMap();

        CreateMap<UserUpdateContract, UserUpdateContractDomain>();
        CreateMap<UserUpdateContract, UserUpdateContractDomain>().ReverseMap();

        CreateMap<User, UserLoginContract>();
        CreateMap<User, UserLoginContract>().ReverseMap();

        CreateMap<User, UserRegisterContract>();
        CreateMap<User, UserRegisterContract>().ReverseMap();

        CreateMap<User, UserGetContract>();
        CreateMap<User, UserGetContract>().ReverseMap();

        CreateMap<User, UserSendContract>();
        CreateMap<User, UserSendContract>().ReverseMap();
    }
  
}
