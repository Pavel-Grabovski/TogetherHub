namespace Domain.Model;

public class Comment : Entity<CommentId>
{
    public required User Author { get; set; }

    public required Topic CurrentTopic { get; set; }

    public required string Text { get; set; }

    public static Comment Create(
        CommentId commentId,
        User author,
        Topic topic,
        string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        return new Comment
        {
            Id = commentId,
            Author = author,
            CurrentTopic = topic, 
            Text = text
        };
    }
}
