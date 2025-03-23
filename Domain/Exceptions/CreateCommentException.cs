namespace Domain.Exceptions;

public class CreateCommentException : CommentException
{
    public CreateCommentException(string message) : base(message)
    {
    }
}
