using Application.User.Commands;
using Application.User.DTOs;
using Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetByIdAsync(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string name)
    {
        var result = await _mediator.Send(new CreateUserCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            name));

        return Ok(result);
    }
}
