namespace Application.Comments.Commands;

public record CreateCommentCommand(Guid TopicId, string Text)
    :ICommand<CreateCommentResult>;
