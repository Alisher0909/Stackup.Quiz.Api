using Microsoft.AspNetCore.Mvc;
using Stackup.Quiz.Api.Dtos;

namespace Stackup.Quiz.Api.Controllers;

[ApiController, Route("api/[controller]")]

public class QuizController : Controller
{
    private static Dictionary<string, QuizDto> quizes = [];
    private static IEnumerable<QuizDto> quizList = quizes.Values?.Where(q => q.State is not QuizState.Deleted) ?? [];
    private static int idIndex = 1;

    [HttpPost]
    public IActionResult Create([FromBody] CreateQuizDto dto)
    {
        if (quizes.TryAdd(dto.Title, new QuizDto
        {
            Id = idIndex++,
            Title = dto.Title,
            Description = dto.Description,
            State = dto.State,
            StartsAt = dto.StartsAt,
            EndsAt = dto.EndsAt,
            IsPrivate = dto.IsPrivate,
            Password = dto.Password
        }) is false)
            return Conflict($"'{dto.Title}' already exists.");

        idIndex++;

        return Ok(quizes[dto.Title]);
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(quizList);


    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var quiz = quizList.FirstOrDefault(q => q.Id == id);
        if (quiz is null)
            return NotFound($"Quiz with ID {id} not found.");

        return Ok(quiz);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateById(int id, [FromBody]UpdateQuizDto dto)
    {
        var quiz = quizList.FirstOrDefault(q => q.Id == id);
        if (quiz is null)
            return NotFound($"Quiz with ID {id} not found.");

        quiz.Title = dto.Title;
        quiz.Description = dto.Description;
        quiz.State = dto.State;
        quiz.StartsAt = dto.StartsAt;
        quiz.EndsAt = dto.EndsAt;
        quiz.IsPrivate = dto.IsPrivate;
        quiz.Password = dto.Password;

        return Ok(quiz);
    }
}