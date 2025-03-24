namespace Application.Comments.Queries;

public record GetCommentsQuery(Guid TopicId) : IQuery<GetCommentsResult>;
