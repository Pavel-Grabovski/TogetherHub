namespace Application.Security.Services;

public interface IJwtSecurityService
{
    public string CreateToken(CustomIdentityUser user);
}
