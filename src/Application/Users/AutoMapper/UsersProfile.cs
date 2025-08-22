using Application.Users.DTOs;
using AutoMapper;
using Domain.Users.Aggregates;

namespace Application.Users.AutoMapper;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();

        CreateMap<Account, AccountDTO>();
        CreateMap<AccountDTO, Account>();
    }
}