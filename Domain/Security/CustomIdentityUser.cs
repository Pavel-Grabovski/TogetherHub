using Microsoft.AspNetCore.Identity;

namespace Domain.Security;

public class CustomIdentityUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? About { get; set; }

}
