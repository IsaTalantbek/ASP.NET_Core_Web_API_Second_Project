using Application.User.DTOs;
using MediatR;

namespace Application.User.Queries;

public class GetAllUsersQuery : IRequest<List<UserDTO>>;