namespace Domain.Security.Dtos;

/// <summary>
/// Login details
/// </summary>
/// <param name="Login"> Email or UserName</param>
/// <param name="Password"></param>
public record LoginRequestDto
(
    string Login,
    string Password
);
