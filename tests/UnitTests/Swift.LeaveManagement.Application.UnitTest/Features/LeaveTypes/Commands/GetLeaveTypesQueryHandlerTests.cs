using AutoMapper;
using FluentAssertions;
using Moq;
using Swift.LeaveManagement.Application.UnitTest.Mocks;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using SwiftHR.LeaveManagement.Application.Interfaces.Logging;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using SwiftHR.LeaveManagement.Application.MappingProfile;

namespace Swift.LeaveManagement.Application.UnitTest.Features.LeaveTypes.Commands;

public class GetLeaveTypesQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockAppLogger;
    private readonly Mock<ILeaveTypeRepository> _mockRepo;

    public GetLeaveTypesQueryHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
        var mapperConfig = new MapperConfiguration(c => { c.AddProfile<LeaveTypeProfile>(); });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        result.Should().BeOfType<List<LeaveTypeDto>>();
        result.Count.Should().Be(3);
    }
}