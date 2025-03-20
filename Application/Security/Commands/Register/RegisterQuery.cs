namespace Application.Security.Commands.Register;

public record RegisterQuery(RegisterUserRequestDto Dto)
    : IQuery<RegisterResult>;
