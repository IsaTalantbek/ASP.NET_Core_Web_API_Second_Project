using Application.User.DTOs;
using MediatR;

namespace Application.User.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<UserDTO?>
{
    public Guid UserId { get; init; }

    public GetUserByIdQuery(Guid userId)
    {
        UserId = userId;
    }
}