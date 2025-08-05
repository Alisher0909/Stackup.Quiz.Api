using Stackup.Quiz.Api.Exceptions;
using Stackup.Quiz.Api.Repositories.Abstractions;

namespace Stackup.Quiz.Api.Repositories;

public class QuizRepository : IQuizRepository
{
    private Dictionary<string, Entities.Quiz> quizes = [];
    private int index = 1;

    public async ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var quiz = await GetSingleAsync(id, cancellationToken)
            ?? throw new CustomNotFoundException("Quiz not found.");

        quiz.State = Entities.QuizState.Deleted;
    }

    public ValueTask<bool> ExistsAsync(string title, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(quizes.ContainsKey(title));

    public ValueTask<IEnumerable<Entities.Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
        => ValueTask.FromResult(quizes.Values.Where(q => q.State != Entities.QuizState.Deleted));


    public async ValueTask<Entities.Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default)
        => await GetSingleOrDefaultAsync(id, cancellationToken)
            ?? throw new CustomNotFoundException("Quiz not found.");

    public ValueTask<Entities.Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(quizes.Values.FirstOrDefault(q => q.Id == id && q.State != Entities.QuizState.Deleted));

    public ValueTask<Entities.Quiz> InsertAsync(Entities.Quiz quiz, CancellationToken cancellationToken = default)
    {
        if (quizes.TryAdd(quiz.Title, quiz))
        {
            quiz.Id = index++;
            return ValueTask.FromResult(quiz);
        }
        
        throw new CustomConflictException("Title must be unique.");
    }

    public async ValueTask<Entities.Quiz> UpdateAsync(int id, Entities.Quiz quiz, CancellationToken cancellationToken = default)
    {
        var found = await GetSingleAsync(id, cancellationToken);
        quiz.Id = found.Id;
        if (found.Title != quiz.Title)
        {
            if (quizes.TryAdd(quiz.Title, quiz))
                quizes.Remove(found.Title);
            else
                throw new CustomConflictException("Title must be unique.");
        }
        else
            quizes[quiz.Title] = quiz;
        return quiz;
    }
}