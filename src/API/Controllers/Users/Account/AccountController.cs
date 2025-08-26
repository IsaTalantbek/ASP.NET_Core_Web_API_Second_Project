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
    private readonly ILogger<AccountController> _logger;

    public AccountController(IMediator mediator, ILogger<AccountController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<AccountDTO>>> GetAll()
    {
        _logger.LogDebug("GetAll");
        return Ok(await _mediator.Send(new GetAllAccountsQuery()));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDTO>> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Get: Id = {id}", id);

        var account = await _mediator.Send(new GetAccountByIdQuery(id));

        if (account == null)
        {
            _logger.LogInformation("NotFound");
            return NotFound();
        }

        _logger.LogInformation("Succesful found");
        return Ok(account);
    }

    [HttpPut]
    public async Task<ActionResult<AccountTransferResponse>> Transfer([FromBody] AccountTransferRequest request)
    {
        _logger.LogInformation("Transfer: From = {From}, To = {To}, Amount = {Amount}", request.From, request.To, request.Amount);

        var result = await _mediator.Send(new AccountTransferCommand(request.From, request.To, request.Amount));

        _logger.LogInformation("Handler result: Type = {FullName}", result.GetType().FullName);

        return result switch
        {
            AccountTransferCommandResult.Success r => Ok(new AccountTransferResponse(
                r.From.BalanceAmount,
                r.Amount)
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
            ),
            AccountTransferCommandResult.SelfTransaction => BadRequest(new ErrorResponse(
                "SelfTransaction",
                "Impossible transfer yo yourself",
                new { })
            ),
            AccountTransferCommandResult.ConcurrencyException => Conflict(new ErrorResponse(
                "ConcurrencyException",
                "Too much concurrency",
                new { })),
            _ => throw new InvalidOperationException($"Invalid result from handler: {result.GetType().FullName}")
        };
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AccountDepositResponse>> Deposit([FromRoute] Guid id, [FromBody] AccountDepositRequest request)
    {
        _logger.LogInformation($"Deposit: id: {id}, amount: {request.Amount}");

        var result = await _mediator.Send(new DepositInAccountCommand(id, request.Amount));

        _logger.LogInformation($"Handler result: {result.GetType().FullName}");

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
            ),
            DepositInAccountCommandResult.ConcurrencyException => Conflict(new ErrorResponse(
                "ConcurrencyException",
                "Too much concurrency",
                new { })
            ),
            _ => throw new InvalidOperationException($"Invalid result from handler: {result.GetType().FullName}")
        };
    }
}