namespace Application.Security.Queries.Login;

public record LoginQuery(LoginRequestDto LoginRequest) 
    : IQuery<LoginResult>;
