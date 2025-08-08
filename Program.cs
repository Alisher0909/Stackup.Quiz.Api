using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Stackup.Quiz.Api;
using Stackup.Quiz.Api.Data;
using Stackup.Quiz.Api.Dtos;
using Stackup.Quiz.Api.Middlewares;
using Stackup.Quiz.Api.Repositories;
using Stackup.Quiz.Api.Repositories.Abstractions;
using Stackup.Quiz.Api.Services;
using Stackup.Quiz.Api.Services.Abstractions;
using Stackup.Quiz.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidationAsyncAutoValidation()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.AllowTrailingCommas = true;
        jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<QuizState>());
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IValidator<CreateQuizDto>, CreateQuizDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateQuizDto>, UpdateQuizDtoValidator>();

builder.Services.AddDbContext<IQuizContext, QuizContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("Quiz"))
    .UseSnakeCaseNamingConvention());

builder.Services.AddHostedService<AutoApplyMigrationsService>();

var app = builder.Build();

app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();