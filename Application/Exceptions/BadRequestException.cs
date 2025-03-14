namespace Application.Exceptions;

public class BadRequestException : ExceptionBase
{
    public override int StatusCode { get; protected set; } = 400;

    public BadRequestException(string message) : base(message)
    {
    }
    public BadRequestException()
    {
    }
}