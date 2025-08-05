namespace Stackup.Quiz.Api.Repositories.Abstractions;

public interface IQuizRepository
{
    ValueTask<Entities.Quiz> InsertAsync(Entities.Quiz quiz, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Entities.Quiz>> GetAllAsync(CancellationToken cancellationToken = default);
    ValueTask<Entities.Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<Entities.Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<Entities.Quiz> UpdateAsync(int id, Entities.Quiz quiz, CancellationToken cancellationToken = default);
    ValueTask<bool> ExistsAsync(string title, CancellationToken cancellationToken = default);
}