using MediatR;
using SwiftHR.LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    public string RequestComments { get; set; } = string.Empty;
}