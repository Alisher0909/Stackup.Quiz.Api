using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stackup.Quiz.Api.Dtos;
using Stackup.Quiz.Api.Models;
using Stackup.Quiz.Api.Services.Abstractions;

namespace Stackup.Quiz.Api.Controllers;

[ApiController, Route("api/[controller]")]

public class QuizController(IQuizService quizService, IMapper mapper) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuizDto dto, CancellationToken abortionToken = default)
    {
        var model = mapper.Map<CreateQuiz>(dto);
        var created = await quizService.CreateQuizAsync(model, abortionToken);
        return Ok(mapper.Map<QuizDto>(created));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken abortionToken = default)
    {
        var quizes = await quizService.GetAllAsync(abortionToken);
        return Ok(quizes.Select(mapper.Map<QuizDto>));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken abortionToken = default)
    {
        var single = await quizService.GetSingleAsync(id, abortionToken);
        return Ok(mapper.Map<QuizDto>(single));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] UpdateQuizDto dto, CancellationToken abortionToken = default)
    {
        var updated = await quizService.UpdateAsync(id, mapper.Map<UpdateQuiz>(dto), abortionToken);
        return Ok(mapper.Map<QuizDto>(updated));
    }
}