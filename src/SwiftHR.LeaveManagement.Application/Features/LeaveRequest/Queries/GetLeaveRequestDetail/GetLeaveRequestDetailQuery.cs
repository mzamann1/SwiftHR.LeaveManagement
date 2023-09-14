using MediatR;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public record GetLeaveRequestDetailQuery(int Id) : IRequest<LeaveRequestDetailsDto>;