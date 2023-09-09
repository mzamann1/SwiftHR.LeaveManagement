using AutoMapper;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using MediatR;
using SwiftHR.LeaveManagement.Application.Exceptions;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public DeleteLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid LeaveType: ", validationResult);

        var model = _mapper.Map<Domain.Entities.LeaveType>(request);

        await _leaveTypeRepository.CreateAsync(model);

        return model.Id;
    }
}