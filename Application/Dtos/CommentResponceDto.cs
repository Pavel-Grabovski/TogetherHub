namespace Application.Dtos;

public class CommentResponseDto
{
    public Guid Id { get; set; }
    public required string Text { get; set; }
    public required string UserId { get; set; }
    public required string Username { get; set; }
    public DateTime CreationTime { get; set; }
}