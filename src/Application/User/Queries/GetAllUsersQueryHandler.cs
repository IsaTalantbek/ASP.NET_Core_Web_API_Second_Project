using Application.System;
using Application.User.DTOs;
using Application.User.Repositories;
using AutoMapper;
using MediatR;

namespace Application.User.Queries;

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
        return _mapper.Map<List<UserDTO>>(await _userRepository.GetAllAsync(ct));
    }
}