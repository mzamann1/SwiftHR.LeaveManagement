using AutoMapper;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using MediatR;
using SwiftHR.LeaveManagement.Application.Exceptions;
using SwiftHR.LeaveManagement.Application.Interfaces.Logging;
using SwiftHR.LeaveManagement.Domain.Entities;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<DeleteLeaveTypeCommandHandler> _logger;
    private readonly IMapper _mapper;

    public DeleteLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<DeleteLeaveTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid LeaveType - {0} :  Name = {1} , DefaultDays = {2} ", nameof(LeaveTypeDto), request.Name, request.DefaultDays);
            throw new BadRequestException("Invalid LeaveType: ", validationResult);
        }

        var model = _mapper.Map<Domain.Entities.LeaveType>(request);

        await _leaveTypeRepository.CreateAsync(model);

        return model.Id;
    }
}