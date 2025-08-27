using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("api/error")]
    public IActionResult HandleError()
    {
        return StatusCode(500, new ErrorResponse("InternalError", "unkown internal error", new {}));
    }
}