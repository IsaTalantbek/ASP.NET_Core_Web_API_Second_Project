using Application.User.DTOs;
using AutoMapper;
using Domain.Users.Aggregates;
using DomainUser = Domain.Users.Aggregates.User;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DomainUser, UserDTO>();
        CreateMap<UserDTO, DomainUser>();

        CreateMap<Account, AccountDTO>();
        CreateMap<AccountDTO, Account>();
    }
}