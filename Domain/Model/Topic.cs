namespace Domain.Model;

public class Topic : Entity<TopicId>
{
    public required string Title { get; set; }
    public DateTime? EventStart { get; set; }
    public required string Summary { get; set; }
    public required string TopicType { get; set; }
    public required Location Location { get; set; }
    public required string AuthorId { get; set; }
    public bool IsVoided { get; set; }
    public List<Relationship> Users { get; set; } = new ();
    public List<Comment> Comments { get; set; } = new ();

    public static Topic Create(
        TopicId id,
        string title,
        DateTime eventStart,
        string summary,
        string topicType,
        string authorId,
        Location location) 
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(summary);
        ArgumentException.ThrowIfNullOrWhiteSpace(topicType);
        ArgumentException.ThrowIfNullOrWhiteSpace(authorId);

        if (eventStart.Kind != DateTimeKind.Utc)
            eventStart = eventStart.ToUniversalTime();

        Topic topic = new Topic
        {
            Id = id,
            Title = title,
            EventStart = eventStart,
            Summary = summary,
            TopicType = topicType,
            Location = location,
            AuthorId = authorId,
            CreationTime = DateTime.UtcNow
        };

        return topic;
    }

    public Topic Update(
        string? title = default,
        string? summary = default,
        string? topicType = default,
        Location? location = default,
        DateTime? eventStart = default
        )
    {
        if(!string.IsNullOrEmpty(title))
            Title = title;

        if(!string.IsNullOrEmpty(summary))
            Summary = summary;

        if(!string.IsNullOrEmpty(topicType))
            TopicType = topicType;

        if(location is not null)
            Location = location;

        if (eventStart.HasValue)
        {
            if(eventStart.Value.Kind != DateTimeKind.Utc)
                eventStart = eventStart.Value.ToUniversalTime();

            EventStart = eventStart.Value;
        }

        return this;
    }
}
