using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using SwiftHR.LeaveManagement.BlazorUI.Services.Base;

namespace SwiftHR.LeaveManagement.BlazorUI.Interfaces;

public interface ILeaveTypeService
{
    Task<List<LeaveTypeVM>> GetLeaveTypes();
    Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
    Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType);
    Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType);
    Task<Response<Guid>> DeleteLeaveType(int id);
}