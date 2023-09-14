using AutoMapper;
using MediatR;
using SwiftHR.LeaveManagement.Application.Interfaces.Identity;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
        IUserService userService)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _userService = userService;
    }


    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request,
        CancellationToken cancellationToken)
    {
        var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsAsync(cancellationToken);
        var requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);


        if (request.IsLoggedInUser)
        {
            var userId = _userService.UserId;
            leaveRequests =
                await _leaveRequestRepository.GetLeaveRequestByUserIdWithDetailsAsync(userId, cancellationToken);

            var employee = await _userService.GetEmployeeByUserIdAsync(userId);
            requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (var req in requests) req.Employee = employee;
        }
        else
        {
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsAsync(cancellationToken);
            requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (var req in requests)
                req.Employee = await _userService.GetEmployeeByUserIdAsync(req.RequestingEmployeeId);
        }

        return requests;
    }
}