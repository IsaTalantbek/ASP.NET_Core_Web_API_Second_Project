using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetAllAccounts;

public class GetAllAccountsQuery : IRequest<List<AccountDTO>>;