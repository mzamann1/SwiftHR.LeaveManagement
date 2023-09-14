using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SwiftHR.LeaveManagement.Domain.Entities;
using SwiftHR.LeaveManagement.Persistence.Data;

namespace SwiftHR.LeaveManagement.Persistence.IntegrationTests;

public class SwiftHRDbContextTests
{
    private readonly SwiftHRDataContext _swiftHRDataContext;

    public SwiftHRDbContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<SwiftHRDataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _swiftHRDataContext = new SwiftHRDataContext(dbOptions);
    }


    [Fact]
    public async void Save_SetDateCreatedValue()
    {
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await _swiftHRDataContext.LeaveTypes.AddAsync(leaveType);
        await _swiftHRDataContext.SaveChangesAsync();


        leaveType.DateCreated.Should().NotBeNull();
    }

    [Fact]
    public async void Save_SetDateModifiedValue()
    {
    }
}