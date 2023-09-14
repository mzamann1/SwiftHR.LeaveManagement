using MediatR;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public record DeleteLeaveTypeCommand(int Id) : IRequest<bool>;