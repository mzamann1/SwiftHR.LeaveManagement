using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using MediatR;
using SwiftHR.LeaveManagement.Application.Exceptions;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, bool>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<bool> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteLeaveTypeCommandValidator();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid LeaveType: ", validationResult);

        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);

        if (leaveType is null)
            throw new NotFoundException(nameof(Domain.Entities.LeaveType), request.Id);

        await _leaveTypeRepository.DeleteAsync(leaveType);
        return true;
    }
}