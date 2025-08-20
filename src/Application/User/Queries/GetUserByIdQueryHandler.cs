using Application.User.DTOs;
using Application.User.Repositories;
using AutoMapper;
using MediatR;

namespace Application.User.Queries;

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
        var user = await _userRepository.GetByIdAsync(query.UserId, ct);

        if (user == null)
            return null;

        return _mapper.Map<UserDTO>(user);
    }
}