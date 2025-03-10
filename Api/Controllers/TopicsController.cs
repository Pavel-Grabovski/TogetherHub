using Application.Dtos;
using Application.Topics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController
    (ITopicsService topicsService) 
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TopicResponseDto>>> GetTopics()
    {
        return Ok(await topicsService.GetTopicsAsync());
    }
}
