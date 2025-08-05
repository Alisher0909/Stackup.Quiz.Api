using Microsoft.EntityFrameworkCore;

namespace Stackup.Quiz.Api.Data;

public class QuizContext(DbContextOptions<QuizContext> options) : DbContext(options)
{
    public DbSet<Entities.Quiz> Quizes { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}