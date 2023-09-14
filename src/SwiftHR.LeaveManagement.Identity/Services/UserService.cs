using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SwiftHR.LeaveManagement.Application.Interfaces.Identity;
using SwiftHR.LeaveManagement.Application.Models.Identity;
using SwiftHR.LeaveManagement.Identity.Models;

namespace SwiftHR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue("uid");

    public async Task<List<Employee>> GetEmployeeListAsync()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        return employees.Select(q => new Employee
        {
            Id = q.Id,
            Email = q.Email,
            FirstName = q.FirstName,
            LastName = q.LastName
        }).ToList();
    }

    public async Task<Employee> GetEmployeeByUserIdAsync(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId);

        return new Employee
        {
            Email = employee.Email,
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };
    }
}