using AspNet.Security.OAuth.Yandex;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Authorize;

[Route("api")]
[ApiController]
public class AuthorizeController : ControllerBase
{

    [HttpGet("signin-yandex")]
    public async Task Signin()
    {
        var context = HttpContext;

        if (HttpContext.User.Identity?.IsAuthenticated ?? false)
        {
            var name = HttpContext.User.Identity.Name;
            await HttpContext.Response.WriteAsync($"Привет, {name}!");
        }
        else
        {
            await context.ChallengeAsync(YandexAuthenticationDefaults.AuthenticationScheme);
        }
    }

    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        return Ok(HttpContext.User.Identity.Name);
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = "api/profile"
        }, YandexAuthenticationDefaults.AuthenticationScheme);
    }

}