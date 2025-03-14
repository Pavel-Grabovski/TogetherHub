namespace Application.Exceptions;

public class UnauthorizedException : ExceptionBase
{
    public override int StatusCode { get; protected set; } = 401;
}
