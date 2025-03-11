namespace Application.Topics.Obsoletes;

[Obsolete("All Obsolete", true)]
public class TopicsService(
    IApplicationDbContext dbContext,
    ILogger<TopicsService> logger,
    IMapper mapper)
//: ITopicsService
{
    public async Task<List<TopicResponseDto>> GetTopicsAsync()
    {
        List<Topic> topicsDb = await dbContext.Topics
            .AsNoTracking()
            .Where(t => !t.IsDelete)
            .ToListAsync();

        List<TopicResponseDto> topicsResponse = mapper.Map<List<TopicResponseDto>>(topicsDb);

        return topicsResponse;
    }


    public async Task<TopicResponseDto> GetTopicByIdAsync(Guid id)
    {
        TopicId topicId = TopicId.Of(id);
        Topic? topicDb = await dbContext.Topics
            .FindAsync(topicId);

        if (topicDb is null || topicDb.IsDelete)
            throw new TopicNotFoundException(id);

        TopicResponseDto responseDto = mapper.Map<TopicResponseDto>(topicDb);

        return responseDto;
    }

    public async Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto dto)
    {
        Topic newTopic = Topic.Create(
            TopicId.Of(Guid.NewGuid()),
            dto.Title,
            dto.EventStart,
            dto.Summary,
            dto.TopicType,
            Location.Of(dto.Location.City, dto.Location.Street));

        dbContext.Topics.Add(newTopic);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        return mapper.Map<TopicResponseDto>(newTopic);
    }

    public async Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicRequestDto topicRequestDto)
    {
        TopicId topicId = TopicId.Of(id);
        Topic? topicDb = await dbContext.Topics
            .FindAsync(topicId);

        if (topicDb is null || topicDb.IsDelete)
            throw new TopicNotFoundException(id);

        topicDb.Title = topicRequestDto.Title ?? topicDb.Title;
        topicDb.Summary = topicRequestDto.Summary ?? topicDb.Summary;
        topicDb.TopicType = topicRequestDto.TopicType ?? topicDb.TopicType;
        topicDb.Location = Location.Of(
            topicRequestDto.Location.City,
            topicRequestDto.Location.Street);

        if (topicRequestDto.EventStart.HasValue
            && topicRequestDto.EventStart.Value.Kind != DateTimeKind.Utc)
        {
            topicDb.EventStart = topicRequestDto.EventStart.Value.ToUniversalTime();
        }

        await dbContext.SaveChangesAsync(CancellationToken.None);

        return mapper.Map<TopicResponseDto>(topicDb);
    }

    public async Task DeleteTopicAsync(Guid id)
    {
        TopicId topicId = TopicId.Of(id);
        Topic? topicDb = await dbContext.Topics
            .FindAsync(topicId);

        if (topicDb is null || topicDb.IsDelete)
            throw new TopicNotFoundException(id);

        topicDb.IsDelete = true;
        topicDb.DeletionTime = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(CancellationToken.None);
    }

}