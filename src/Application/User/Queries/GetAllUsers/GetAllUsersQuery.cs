using Application.User.DTOs;
using MediatR;

namespace Application.User.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<List<UserDTO>>;