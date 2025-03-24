namespace Application.Comments.Queries;

public class GetCommentsHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetCommentsQuery, GetCommentsResult>
{
    public async Task<GetCommentsResult> Handle(
        GetCommentsQuery request,
        CancellationToken cancellationToken)
    {
        TopicId topicId = TopicId.Of(request.TopicId);
        List<Comment> comments = await dbContext.Comments
            .Where(c => c.CurrentTopicId == topicId && !c.IsDelete)
            .Include(c => c.Author)
            .OrderByDescending(c => c.CreationTime)
            .ToListAsync();

        return new GetCommentsResult(comments.ToCommentsResponseDto());
    }
}
