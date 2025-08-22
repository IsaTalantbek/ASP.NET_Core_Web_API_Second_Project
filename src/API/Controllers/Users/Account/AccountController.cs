using API.Controllers.Users.Account.Requests;
using API.Controllers.Users.Account.Responses;
using Application.Users.Commands.DepositInAccount;
using Application.Users.Commands.UserTransfer;
using Application.Users.DTOs;
using Application.Users.Queries.GetAccountById;
using Application.Users.Queries.GetAllAccounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Users.Account;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<AccountDTO>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllAccountsQuery()));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDTO>> GetByIdAsync(Guid id)
    {
        var account = await _mediator.Send(new GetAccountByIdQuery(id));

        if (account == null)
            return NotFound();

        return Ok(account);
    }

    [HttpPut]
    public async Task<ActionResult<AccountTransferResponse>> Transfer([FromBody] AccountTransferRequest request)
    {
        var result = await _mediator.Send(new AccountTransferCommand(request.From, request.To, request.Amount));

        return result switch
        {
            AccountTransferCommandResult.Success r => Ok(new AccountTransferResponse(
                r.To.BalanceAmount)
            ),
            AccountTransferCommandResult.NegativeAmount r => BadRequest(new ErrorResponse(
                "NegativeAmount",
                $"Negative amount: {r.Amount}",
                new { r.Amount })
            ),
            AccountTransferCommandResult.NotFound r => NotFound(new ErrorResponse(
                    "NotFound",
                    $"Account {r.AccountId} not found",
                    new { r.AccountId })
            ),
            AccountTransferCommandResult.NegativeBalance r => BadRequest(new ErrorResponse(
                   "NegativeBalance",
                   "After transfer, balance will become negative",
                   new { r.NegativeBalanceAccount.BalanceAmount, r.Amount })
            )
        };
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AccountDepositResponse>> Deposit([FromRoute] Guid id, [FromBody] AccountDepositRequest request)
    {
        var result = await _mediator.Send(new DepositInAccountCommand(id, request.Amount));

        return result switch
        {
            DepositInAccountCommandResult.Success r => Ok(new AccountDepositResponse(
                r.Account.BalanceAmount)
            ),
            DepositInAccountCommandResult.NotFound r => NotFound(new ErrorResponse(
                "NotFound",
                $"Account {r.AccountId} not found",
                new { AccountId = r.AccountId })
            ),
            DepositInAccountCommandResult.NegativeAmount r => BadRequest(new ErrorResponse(
                "NegativeAmount",
                $"Negative amount: {r.Amount}",
                new {Amount = r.Amount})
            )
        };
    }
}