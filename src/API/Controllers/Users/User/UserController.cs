using API.Controllers.Users.User.Requests;
using API.Controllers.Users.User.Responses;
using Application.Users.Commands.CreateUser;
using Application.Users.DTOs;
using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Users.User;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetAll()
    {
        _logger.LogDebug("GetAll");
        return Ok(await _mediator.Send(new GetAllUsersQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Get: Id = {id}", id);
        var user = await _mediator.Send(new GetUserByIdQuery(id));

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserCreateResponse>> Create([FromBody] UserCreateRequest request)
    {
        _logger.LogInformation("Create: Name = {Name}", request.Name);

        var result = await _mediator.Send(new CreateUserCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            request.Name));

        _logger.LogInformation("Handler result: Type = {FullName}", result.GetType().FullName);

        return result switch
        {
            CreateUserCommandResult.Success r => Ok(new UserCreateResponse(
                r.CreatedUserGuid,
                r.CreatedAccountGuid)
            )
        };
    }
}
