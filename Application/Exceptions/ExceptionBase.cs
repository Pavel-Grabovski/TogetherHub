namespace Application.Exceptions;

public abstract class ExceptionBase : Exception
{
    public virtual int StatusCode { get; protected set; } = 500;

    public ExceptionBase(string message) : base(message)
    {
    }
    public ExceptionBase()
    {
    }

}
