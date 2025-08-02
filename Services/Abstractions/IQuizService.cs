using Stackup.Quiz.Api.Models;

namespace Stackup.Quiz.Api.Services.Abstractions;

public interface IQuizService
{
    ValueTask<Models.Quiz> CreateQuizAsync(CreateQuiz quiz, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Models.Quiz>> GetAllAsync(CancellationToken cancellationToken = default);
    ValueTask<Models.Quiz> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<bool> TryDeleteAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<Models.Quiz> UpdateAsync(UpdateQuiz quiz, CancellationToken cancellationToken = default); 
}