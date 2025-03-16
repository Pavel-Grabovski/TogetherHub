namespace Application.Topics.Queries.GetTopic;

public class GetTopicHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetTopicQuery, GetTopicResult>
{
    public async Task<GetTopicResult> Handle(GetTopicQuery request, CancellationToken cancellationToken)
    {
        TopicId topicId = TopicId.Of(request.Id);
        Topic? topicDb = await dbContext.Topics
            .AsNoTracking()
            .Include(t => t.Users)
            .ThenInclude(r => r.CurrentUser)
            .FirstOrDefaultAsync(t => t.Id == topicId);

        if (topicDb is null || topicDb.IsDelete)
            throw new TopicNotFoundException(request.Id);

        TopicResponseDto responseDto = topicDb.ToTopicResponseDto();

        return new GetTopicResult(responseDto);
    }
}
