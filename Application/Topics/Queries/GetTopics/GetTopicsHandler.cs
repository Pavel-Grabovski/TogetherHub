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
          .Include(t => t.Users)
          .ThenInclude(r => r.CurrentUser)
          .Where(t => !t.IsDelete)
          .ToListAsync(cancellationToken);

        List<TopicResponseDto> topicsResponse =
            topicsDb.ToTopicResponseDtoList();

        return new GetTopicsResult(topicsResponse);
    }
}