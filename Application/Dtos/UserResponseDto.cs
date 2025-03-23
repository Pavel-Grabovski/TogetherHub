namespace Application.Dtos;

public record UserResponseDto
(
    string UserName,
    string Email,
    string JwtToken
);
