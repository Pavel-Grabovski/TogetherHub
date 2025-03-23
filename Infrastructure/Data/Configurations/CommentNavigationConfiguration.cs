namespace Infrastructure.Data.Configurations;

public class CommentNavigationConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(t => t.CurrentTopic)
            .WithMany(c => c.Comments)
            .HasForeignKey(t => t.CurrentTopicId);
    }
}