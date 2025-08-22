using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<List<UserDTO>>;