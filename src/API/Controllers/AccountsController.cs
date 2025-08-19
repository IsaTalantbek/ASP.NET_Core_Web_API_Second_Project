using API.Controllers.Requests;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDTO>> GetByIdAsync(Guid id)
    {
        var account = await _mediator.Send(new GetAccountByIdQuery(id));

        if (account == null)
            return NotFound();

        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> Transfer([FromBody] AccountsTransferRequest request)
    {
        await _mediator.Send(new UserTransferCommand(request.From, request.To, request.Amount));

        return Ok();
    }
}