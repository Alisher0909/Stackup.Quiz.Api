namespace Stackup.Quiz.Api.Entities;

public interface IHasTimestamp
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset UpdatedAt { get; set; }
}