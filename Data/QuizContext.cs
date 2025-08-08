using Microsoft.EntityFrameworkCore;
using Stackup.Quiz.Api.Entities;

namespace Stackup.Quiz.Api.Data;

public class QuizContext(DbContextOptions<QuizContext> options) : DbContext(options), IQuizContext
{
    public DbSet<Entities.Quiz> Quizes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTimestampChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyTimestampChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry is { State: EntityState.Added, Entity: IHasTimestamp added })
            {
                added.CreatedAt = DateTimeOffset.UtcNow;
                added.UpdatedAt = DateTimeOffset.UtcNow;
            }
            else if (entry is { State: EntityState.Modified, Entity: IHasTimestamp updated })
                updated.UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}