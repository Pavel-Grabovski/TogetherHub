using Microsoft.AspNetCore.Identity;

namespace Domain.Security;

public class User : IdentityUser
{
    public string? FullName { get; set; }
    public string? About { get; set; }

}
