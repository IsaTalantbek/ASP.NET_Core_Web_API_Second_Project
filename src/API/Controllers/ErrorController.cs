using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)] // Это исключит контроллер из документации Swagger
[Route("api/error")]
public class ErrorController : ControllerBase
{
    public IActionResult HandleError()
    {
        return StatusCode(500, new ErrorResponse("InternalError", "unkown internal error", new {}));
    }
}