namespace Stackup.Quiz.Api.Models;

public record UpdateQuiz(
    string Title,
    string? Description, 
    QuizState State,
    DateTimeOffset? StartsAt,
    DateTimeOffset? EndsAt,
    bool IsPrivate,
    string? Password);