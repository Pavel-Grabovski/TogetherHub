namespace Application.Security.Services;

public interface IJwtSecurityService
{
    public string CreateToken(User user);
}
