namespace Application.Topics.Queries.GetTopics;

public class GetTopicsHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetTopicsQuery, GetTopicsResult>
{
    public async Task<GetTopicsResult> Handle(
        GetTopicsQuery request,
        CancellationToken cancellationToken)
    {
        List<Topic> topicsDb = await dbContext.Topics
          .AsNoTracking()
          .Where(t => !t.IsDelete)
          .ToListAsync();

        List<TopicResponseDto> topicsResponse =
            topicsDb.ToTopicResponseDtoList();

        return new GetTopicsResult(topicsResponse);
    }
}