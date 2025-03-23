namespace Application.Dtos;

public record RegisterUserRequestDto
(
    string UserName,
    string Email,
    string Password
);