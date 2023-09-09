using AutoMapper;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using MediatR;
using SwiftHR.LeaveManagement.Application.Exceptions;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypesDetailQueryHandler : IRequestHandler<GetLeaveTypesDetailQuery, LeaveTypeDetailDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypesDetailQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<LeaveTypeDetailDto> Handle(GetLeaveTypesDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);

        if (leaveType is null)
            throw new NotFoundException(nameof(Domain.Entities.LeaveType), request.Id);

        var data = _mapper.Map<LeaveTypeDetailDto>(leaveType);

        return data;
    }
}