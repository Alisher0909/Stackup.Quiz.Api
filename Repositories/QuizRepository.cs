using Microsoft.EntityFrameworkCore;
using Npgsql;
using Stackup.Quiz.Api.Data;
using Stackup.Quiz.Api.Exceptions;
using Stackup.Quiz.Api.Repositories.Abstractions;

namespace Stackup.Quiz.Api.Repositories;

public class QuizRepository(QuizContext context) : IQuizRepository
{
    public async ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var effectedRows = await context.Quizes.Where(q => q.Id == id).ExecuteDeleteAsync(cancellationToken);
        if (effectedRows < 1)
            throw new CustomNotFoundException($"Quiz with id {id} not found.");
    }

    public async ValueTask<bool> ExistsAsync(string title, CancellationToken cancellationToken = default)
        => await context.Quizes.AnyAsync(q => q.Title == title, cancellationToken);

    public async ValueTask<IEnumerable<Entities.Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Quizes.Where(q => q.State != Entities.QuizState.Deleted).ToListAsync(cancellationToken);

    public async ValueTask<Entities.Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default)
        => await GetSingleOrDefaultAsync(id, cancellationToken)
            ?? throw new CustomNotFoundException("Quiz not found.");

    public async ValueTask<Entities.Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default)
        => await context.Quizes.FindAsync([ id ], cancellationToken);

    public async ValueTask<Entities.Quiz> InsertAsync(Entities.Quiz quiz, CancellationToken cancellationToken = default)
    {
        try
        {
            var entry = context.Quizes.Add(quiz);
            await context.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
        {
            throw new CustomConflictException("Title must be unique.");
        }
    }

    public async ValueTask<Entities.Quiz> UpdateAsync(Entities.Quiz quiz, CancellationToken cancellationToken = default)
    {
        try
        {
            quiz.UpdatedAt = DateTimeOffset.UtcNow;
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
        {
            throw new CustomConflictException("Title must be unique.");
        }

        return quiz;
    }
}