namespace Application.Dtos;

public record UserProfileResponseDto
(
    string Id,
    string UserName,
    string Email,
    string? FullName,
    string Role
);

