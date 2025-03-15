using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Application.Security.Services;

public class UserAccessor(IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    public string GetUsername()
    {
        return httpContextAccessor
            .HttpContext!
            .User
            .FindFirstValue("name")!;
    }
}