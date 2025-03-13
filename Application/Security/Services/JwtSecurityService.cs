using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Application.Security.Services;

public class JwtSecurityService(IConfiguration configuration)
    : IJwtSecurityService
{
    public string CreateToken(CustomIdentityUser user)
    {
        string secretKey = configuration["AuthSettings:SecretKey"]!;

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("IsPremium", "true"),
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(secretKey));

        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenHandler = new JsonWebTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = creds,
            Subject = new ClaimsIdentity(claims),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow.AddMinutes(0),
            Expires = DateTime.UtcNow.AddMinutes(15),
        };

        string token = tokenHandler.CreateToken(tokenDescriptor);

        return token;
    }
}