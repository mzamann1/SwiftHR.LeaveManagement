using AutoMapper;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Domain.Entities.LeaveType>(request);

        await _leaveTypeRepository.UpdateAsync(model);

        return Unit.Value;
    }
}