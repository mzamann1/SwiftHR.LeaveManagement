using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveAllocations;

namespace SwiftHR.LeaveManagement.BlazorUI.Models.LeaveRequests;

public class EmployeeLeaveRequestViewVM
{
    public List<LeaveAllocationVM> LeaveAllocations { get; set; } = new();
    public List<LeaveRequestVM> LeaveRequests { get; set; } = new();
}