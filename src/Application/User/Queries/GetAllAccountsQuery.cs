using Application.User.DTOs;
using MediatR;

namespace Applcation.User.Queries;

public class GetAllAccountsQuery : IRequest<List<AccountDTO>>;