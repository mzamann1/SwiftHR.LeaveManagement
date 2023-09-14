using SwiftHR.LeaveManagement.BlazorUI.Services.Base;

namespace SwiftHR.LeaveManagement.BlazorUI.Interfaces;

public interface ILeaveAllocationService
{
    Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId);
}