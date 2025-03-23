namespace Application.Extensions;

public static class CommentExtensions
{
    public static CommentResponseDto ToCommentResponseDto(
        this Comment comment)
    {
        return new CommentResponseDto
        { 
            Id = comment.Id.Value,
            Text = comment.Text,
            UserId = comment.Author.Id,
            Username = comment.Author.UserName!,
            CreationTime = comment.CreationTime
        };
    }
}