using Application.User.DTOs;
using MediatR;

namespace Application.User.Queries.GetAllAccounts;

public class GetAllAccountsQuery : IRequest<List<AccountDTO>>;