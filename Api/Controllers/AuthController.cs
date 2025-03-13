using Api.Security.Services;
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
    UserManager<CustomIdentityUser> userManager,
    IJwtSecurityService jwtSecurityService) 
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequestDto dto)
    {
        CustomIdentityUser? user = await userManager.FindByEmailAsync(dto.Login);

        if (user == null)
            return Results.Unauthorized();

        bool result = await userManager.CheckPasswordAsync(user, dto.Password);

        if (result)
        {
            IdentityUserResponseDto response = new IdentityUserResponseDto(
                user.UserName,
                user.Email,
                jwtSecurityService.CreateToken(user));

            return Results.Ok(new {result = response});
        }

        return Results.Unauthorized();
    }

    [HttpPost("register")]
    public async Task<IResult> Register(RegisterUserRequestDto dto)
    {
        if (await userManager.FindByEmailAsync(dto.Email) != null)
            return Results.BadRequest("A user with this email already exists");

        if (await userManager.FindByEmailAsync(dto.UserName) != null)
            return Results.BadRequest("A user with this UserName already exists");

        var user = new CustomIdentityUser
        {
            Email = dto.Email,
            UserName = dto.UserName,
        };

        IdentityResult result = await userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            var response = new IdentityUserResponseDto(
               user.UserName,
               user.Email,
               jwtSecurityService.CreateToken(user));

            return Results.Ok(new { result = response });
        }

        return Results.BadRequest(result.Errors);
    }
}
