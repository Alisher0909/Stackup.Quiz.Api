namespace Stackup.Quiz.Api.Exceptions;

public class CustomNotFoundException(string errorMessage) 
    : Exception(errorMessage) { }