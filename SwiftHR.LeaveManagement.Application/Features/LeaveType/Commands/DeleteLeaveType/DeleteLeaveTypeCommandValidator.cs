using FluentValidation;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

internal class DeleteLeaveTypeCommandValidator : AbstractValidator<DeleteLeaveTypeCommand>
{
    public DeleteLeaveTypeCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be non negative");
    }
}