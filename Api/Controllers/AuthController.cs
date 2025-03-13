using Domain.Security;
using Domain.Security.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(UserManager<CustomIdentityUser> userManager) 
    : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<IResult> Login(LoginResponseDto dto)
    {
        CustomIdentityUser? user = await userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return Results.Unauthorized();

        bool result = await userManager.CheckPasswordAsync(user, dto.Password);

        if (result)
        {
            IdentityUserResponseDto response = new IdentityUserResponseDto(
                user.UserName,
                user.Email,
                "jwt");

            return Results.Ok(new {result = response});
        }

        return Results.Unauthorized();
    }
}
