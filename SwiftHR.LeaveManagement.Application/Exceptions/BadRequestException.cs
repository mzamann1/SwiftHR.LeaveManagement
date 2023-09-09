using FluentValidation.Results;

namespace SwiftHR.LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = new List<string>();
        foreach (var error in validationResult.Errors) ValidationErrors.Add(error.ErrorMessage);
    }

    public List<string> ValidationErrors { get; set; }
}