namespace Application.Topics.Queries.GetTopics;

public class GetTopicsHandler(
    IApplicationDbContext dbContext,
    IMapper mapper)
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
            mapper.Map<List<TopicResponseDto>>(topicsDb);

        return new GetTopicsResult(topicsResponse);
    }
}