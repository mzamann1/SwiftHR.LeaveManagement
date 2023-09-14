using SwiftHR.LeaveManagement.Application.Models.Identity;

namespace SwiftHR.LeaveManagement.Application.Interfaces.Identity;

public interface IUserService
{
    public string UserId { get; }
    Task<List<Employee>> GetEmployeeListAsync();
    Task<Employee> GetEmployeeByUserIdAsync(string userId);
}