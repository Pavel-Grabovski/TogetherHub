using Domain.Security;

namespace Api.Security.Services;

public interface IJwtSecurityService
{
    public string CreateToken(CustomIdentityUser user);
}
