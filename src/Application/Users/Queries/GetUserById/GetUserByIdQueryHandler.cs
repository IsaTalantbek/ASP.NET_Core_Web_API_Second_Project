using Application.Users.DTOs;
using Application.Users.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO?>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserDTO?> Handle(GetUserByIdQuery query, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId, isReadonly: true, ct);

        if (user == null)
            return null;

        return _mapper.Map<UserDTO>(user);
    }
}