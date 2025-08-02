namespace Stackup.Quiz.Api.Models;

public record CreateQuiz(
    string Title,
    string? Description, 
    QuizState State,
    DateTimeOffset? StartsAt,
    DateTimeOffset? EndsAt,
    bool IsPrivate,
    string? Password);
