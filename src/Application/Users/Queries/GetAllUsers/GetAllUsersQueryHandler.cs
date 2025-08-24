using Application.System;
using Application.Users.DTOs;
using Application.Users.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    
    public GetAllUsersQueryHandler(IMapper mapper,IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<List<UserDTO>> Handle(GetAllUsersQuery _, CancellationToken ct)
    {
        return _mapper.Map<List<UserDTO>>(await _userRepository.GetAllAsync(isReadonly: true, ct));
    }
}