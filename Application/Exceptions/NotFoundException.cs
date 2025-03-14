namespace Application.Exceptions;

public class NotFoundException : ExceptionBase
{
    public override int StatusCode { get; protected set; } = 404;

    public NotFoundException(string message)
        : base(message)
    {
    }
}
