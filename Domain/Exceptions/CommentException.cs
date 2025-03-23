namespace Domain.Exceptions;

public class CommentException : DomainException
{
    public CommentException(string message) : base(message)
    {
    }
}
