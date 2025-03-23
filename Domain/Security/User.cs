using Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace Domain.Security;

public class User : IdentityUser
{
    public string? FullName { get; set; }
    public string? About { get; set; }

    public List<Relationship> Topics { get; set; } = new();
}
