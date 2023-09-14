using FluentValidation;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandValidator : AbstractValidator<DeleteLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public DeleteLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;


        RuleFor(p => p.Id)
            .NotNull()
            .WithMessage("{PropertyName} must be provided");
    }
}