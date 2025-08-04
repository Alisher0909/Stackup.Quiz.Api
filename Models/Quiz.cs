namespace Stackup.Quiz.Api.Models;

public record Quiz(
    string Title,
    string? Description,
    QuizState State,
    DateTimeOffset? StartsAt,
    DateTimeOffset? EndsAt,
    bool IsPrivate,
    string? Password)

{
    public int Id { get; set; }
}