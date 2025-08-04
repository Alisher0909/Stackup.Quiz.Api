using AutoMapper;
using Stackup.Quiz.Api.Exceptions;
using Stackup.Quiz.Api.Models;
using Stackup.Quiz.Api.Services.Abstractions;

namespace Stackup.Quiz.Api.Services;

public class QuizService(IMapper mapper) : IQuizService
{
    private readonly Dictionary<string, Models.Quiz> quizes = [];
    private IEnumerable<Models.Quiz> existingQuizes =>
        quizes.Values.Where(q => q.State is not QuizState.Deleted);
    private int idIndex = 1;

    public ValueTask<Models.Quiz> CreateQuizAsync(CreateQuiz quiz, CancellationToken cancellationToken = default)
    {
        if (quizes.ContainsKey(quiz.Title))
            throw new CustomConflictException($"Quiz with title '{quiz.Title}' already exists.");

        var newQuiz = mapper.Map<Models.Quiz>(quiz);
        newQuiz.Id = idIndex++;

        quizes.Add(quiz.Title, newQuiz);

        return ValueTask.FromResult(newQuiz);
    }

    public ValueTask<IEnumerable<Models.Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
        => ValueTask.FromResult(existingQuizes);

    public ValueTask<Models.Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(existingQuizes.FirstOrDefault(q => q.Id == id));

    public async ValueTask<Models.Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default)
        => await GetSingleOrDefaultAsync(id, cancellationToken)
            ?? throw new CustomNotFoundException($"Quiz with ID {id} not found.");

    public async ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var quiz = await GetSingleAsync(id, cancellationToken);
        quizes[quiz.Title] = quiz with { State = QuizState.Deleted };
    }

    public async ValueTask<Models.Quiz> UpdateAsync(int id, UpdateQuiz quiz, CancellationToken cancellationToken = default)
    {
        var quizToUpdate = await GetSingleAsync(id, cancellationToken);
        mapper.Map(quiz, quizToUpdate);

        return quizToUpdate;
    }
    
    public ValueTask<bool> ExistsAsync(string title, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(quizes.ContainsKey(title));
}