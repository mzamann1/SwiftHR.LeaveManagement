﻿using FluentValidation;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

internal class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be non negative");


        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");


        RuleFor(p => p.DefaultDays)
            .GreaterThan(100).WithMessage("{PropertyName} cannot exceed 100 characters")
            .LessThan(1).WithMessage("{PropertyName} must be at least 1 character");


        RuleFor(q => q).MustAsync(LeaveTypeNameUnique).WithMessage("Leave type already exists");

        RuleFor(q => q.Id).MustAsync(LeaveTypeMustExists).WithMessage("Leave type doesn't exist with Id:");


        _leaveTypeRepository = leaveTypeRepository;
    }

    private Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }

    private async Task<bool> LeaveTypeMustExists(int id, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}