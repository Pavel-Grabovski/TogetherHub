using Application.Security.Commands.Register;
using Application.Security.Queries.Login;
using Application.Security.Services;
using Domain.Security;
using Domain.Security.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IMediator mediator,
    UserManager<CustomIdentityUser> userManager,
    IJwtSecurityService jwtSecurityService) 
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequestDto dto)
    {                   
        return Results.Ok(await mediator.Send(new LoginQuery(dto)));
    }

    [HttpPost("register")]
    public async Task<IResult> Register(RegisterUserRequestDto dto)
    {
        return Results.Ok(await mediator.Send(new RegisterQuery(dto)));
    }
}
