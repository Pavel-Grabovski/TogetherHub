namespace Application.Exceptions;

public class ForbiddenException : ExceptionBase
{
    public override int StatusCode { get; protected set; } = 403;

    public ForbiddenException(string message) : base(message)
    {
    }
    public ForbiddenException()
    {
    }
}
