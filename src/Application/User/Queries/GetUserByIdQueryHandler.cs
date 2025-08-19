using Application.User.DTOs;
using Application.User.Repositories;
using MediatR;

namespace Application.User.Queries;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDTO?> Handle(GetUserByIdQuery query, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);

        if (user == null)
            return null;

        return new UserDTO(user.Id, user.AccountId, user.Name, user.Address);
    }
}