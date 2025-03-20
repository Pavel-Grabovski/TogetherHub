using Application.Security.Commands.Register;
using Application.Security.Queries.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) 
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequestDto dto, CancellationToken cancellationToken)
    {                   
        return Results.Ok(await mediator.Send(new LoginQuery(dto), cancellationToken));
    }

    [HttpPost("register")]
    public async Task<IResult> Register(RegisterUserRequestDto dto, CancellationToken cancellationToken)
    {
        return Results.Ok(await mediator.Send(new RegisterCommand(dto), cancellationToken));
    }
}
