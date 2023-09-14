using AutoMapper;
using MediatR;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class
    GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var leaveAllocation =
            await _leaveAllocationRepository.GetLeaveAllocationsWithDetails(request.Id, cancellationToken);
        return _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
    }
}