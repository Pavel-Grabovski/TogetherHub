namespace Infrastructure.Data.Configurations;

public class CommentIdConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(item => item.Id)
            .HasConversion(
                id => id.Value,
                value => CommentId.Of(value)
            );
    }
}