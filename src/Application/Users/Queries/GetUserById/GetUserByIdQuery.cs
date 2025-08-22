using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<UserDTO?>
{
    public Guid UserId { get; init; }

    public GetUserByIdQuery(Guid userId)
    {
        UserId = userId;
    }
}