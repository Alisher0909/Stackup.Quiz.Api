namespace Stackup.Quiz.Api.Dtos;

public class CreateQuizDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public QuizState State { get; set; }
    public DateTimeOffset? StartsAt { get; set; }
    public DateTimeOffset? EndsAt { get; set; }
    public bool IsPrivate { get; set; }
    public string? Password { get; set; }
}