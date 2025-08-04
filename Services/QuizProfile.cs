using AutoMapper;
using Stackup.Quiz.Api.Dtos;
using Stackup.Quiz.Api.Models;

namespace Stackup.Quiz.Api.Services;

public class QuizProfile : Profile
{
    public QuizProfile()
    {
        CreateMap<CreateQuizDto, CreateQuiz>();
        CreateMap<UpdateQuizDto, UpdateQuiz>();
        CreateMap<QuizDto, Models.Quiz>();
        CreateMap<Dtos.QuizState, Models.QuizState>();

        CreateMap<CreateQuiz, Models.Quiz>();
        CreateMap<UpdateQuiz, Models.Quiz>();
        CreateMap<Models.Quiz, QuizDto>();
    }
}