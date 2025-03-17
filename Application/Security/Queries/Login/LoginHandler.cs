namespace Application.Security.Queries.Login;

public class LoginHandler(
    UserManager<User> userManager,
    IJwtSecurityService jwtSecurityService)
    : IQueryHandler<LoginQuery, LoginResult>
{
    public async Task<LoginResult> Handle(
        LoginQuery request,
        CancellationToken cancellationToken)
    {
        User? user = await userManager.Users
            .FirstOrDefaultAsync(
              u => request.LoginRequest.Login.ToUpper() == u.NormalizedEmail 
                || request.LoginRequest.Login.ToUpper() == u.NormalizedUserName, 
              cancellationToken);

        if (user == null)
            throw new UnauthorizedException();

        bool result = await userManager
            .CheckPasswordAsync(user, request.LoginRequest.Password);

        if (!result)
            throw new UnauthorizedException();

        IdentityUserResponseDto response = new IdentityUserResponseDto(
                user.UserName,
                user.Email,
                jwtSecurityService.CreateToken(user));

        return new LoginResult(response);

    }
}