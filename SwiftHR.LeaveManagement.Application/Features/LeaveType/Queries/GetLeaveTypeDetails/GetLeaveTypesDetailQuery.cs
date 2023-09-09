using MediatR;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public record GetLeaveTypesDetailQuery(int Id) : IRequest<LeaveTypeDetailDto>;