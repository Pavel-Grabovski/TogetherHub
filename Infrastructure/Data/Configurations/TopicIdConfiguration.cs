using Domain.Model;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastructure.Data.Configurations;

public class TopicIdConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => TopicId.Of(value)
        );
    }
}
