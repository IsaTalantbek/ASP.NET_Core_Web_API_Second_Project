using API.Controllers.Requests;
using Applcation.User.Queries;
using Application.User.Commands;
using Application.User.DTOs;
using Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
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
    public async Task<IActionResult> Transfer([FromBody] AccountsTransferRequest request)
    {
        await _mediator.Send(new UserTransferCommand(request.From, request.To, request.Amount));

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Deposit([FromRoute] Guid id, [FromBody] long amount)
    {
        await _mediator.Send(new DepositInAccountCommand(id, amount));

        return Ok();
    }
}