using AutoMapper;
using MediatR;
using SwiftHR.LeaveManagement.Application.Exceptions;
using SwiftHR.LeaveManagement.Application.Interfaces.Identity;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
        IUserService userService)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _userService = userService;
    }


    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request,
        CancellationToken cancellationToken)
    {
        var leaveRequest =
            _mapper.Map<LeaveRequestDetailsDto>(
                await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.Id, cancellationToken));

        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        // Add Employee details as needed
        leaveRequest.Employee = await _userService.GetEmployeeByUserIdAsync(leaveRequest.RequestingEmployeeId);

        return leaveRequest;
    }
}