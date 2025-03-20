namespace Application.Security.Commands.Register;

public record RegisterCommand(RegisterUserRequestDto Dto)
    : ICommand<RegisterResult>;
