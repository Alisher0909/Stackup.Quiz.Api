using Stackup.Quiz.Api.Models;
using Stackup.Quiz.Api.Services.Abstractions;

namespace Stackup.Quiz.Api.Services;

public class QuizService : IQuizService
{
    private readonly Dictionary<string, Models.Quiz> quizzes = new();
    private int idIndex = 1;

    public ValueTask<Models.Quiz> CreateQuizAsync(CreateQuiz quiz, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<Models.Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Models.Quiz> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> TryDeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Models.Quiz> UpdateAsync(UpdateQuiz quiz, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}