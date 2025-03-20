namespace Application.Security.Commands.Register;

public class RegisterHandler(
    UserManager<User> userManager,
    IJwtSecurityService jwtSecurityService)
    : ICommandHandler<RegisterCommand, RegisterResult>
{
    public async Task<RegisterResult> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        if (await userManager.FindByEmailAsync(request.Dto.Email) != null)
            throw new BadRequestException("A user with this email already exists");

        if (await userManager.FindByNameAsync(request.Dto.UserName) != null)
            throw new BadRequestException("A user with this UserName already exists");

        var user = new User
        {
            Email = request.Dto.Email,
            UserName = request.Dto.UserName
        };

        IdentityResult result = await userManager
            .CreateAsync(user, request.Dto.Password);

        if (!result.Succeeded)
            throw new BadRequestException(
                string.Join(";", result.Errors.Select(e => e.Description)));

        var response = new UserResponseDto(
               user.UserName,
               user.Email,
               jwtSecurityService.CreateToken(user));

        return new RegisterResult(response);
    }
}
