namespace Stackup.Quiz.Api.Exceptions;

public class CustomConflictException(string errorMessage) 
    : Exception(errorMessage) { }
