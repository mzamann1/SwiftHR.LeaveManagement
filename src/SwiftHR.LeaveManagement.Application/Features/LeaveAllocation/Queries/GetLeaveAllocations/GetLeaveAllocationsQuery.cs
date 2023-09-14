using MediatR;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationsQuery : IRequest<List<LeaveAllocationDto>>
{
}