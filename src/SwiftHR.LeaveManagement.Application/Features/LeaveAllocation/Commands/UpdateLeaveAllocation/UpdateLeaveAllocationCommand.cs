using MediatR;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public record UpdateLeaveAllocationCommand(int Id, int NumberOfDays, int LeaveTypeId, int Period) : IRequest<Unit>;