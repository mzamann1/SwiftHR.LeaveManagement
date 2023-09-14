using MediatR;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery(bool IsLoggedInUser) : IRequest<List<LeaveRequestListDto>>;