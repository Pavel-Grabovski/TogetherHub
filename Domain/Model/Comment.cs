namespace Domain.Model;

public class Comment : Entity<CommentId>
{
    public required User Author { get; set; }
    public required string AuthorId { get; set; }

    public required Topic CurrentTopic { get; set; }
    public required TopicId CurrentTopicId { get; set; }

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
            AuthorId = author.Id,
            CurrentTopic = topic, 
            CurrentTopicId = topic.Id,
            Text = text,
            CreationTime = DateTime.UtcNow
        };
    }
}
