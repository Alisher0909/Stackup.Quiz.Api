using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stackup.Quiz.Api.Entities.Configurations;

public class QuizConfigurations : IEntityTypeConfiguration<Entities.Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.HasKey(q => q.Id);

        builder.HasIndex(q => q.Title)
            .IsUnique();

        builder.Property(q => q.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(q => q.Description)
            .HasMaxLength(500);

        builder.Property(q => q.CreatedAt);

        builder.Property(q => q.UpdatedAt);

        builder.Property(q => q.Password)
            .HasMaxLength(6)
            .IsFixedLength();
    }
}