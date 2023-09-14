using Microsoft.EntityFrameworkCore;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using SwiftHR.LeaveManagement.Domain.Entities;
using SwiftHR.LeaveManagement.Persistence.Data;

namespace SwiftHR.LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(SwiftHRDataContext swiftHRDataContext) : base(swiftHRDataContext)
    {
    }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        return !await _context.LeaveTypes.AnyAsync(lt => lt.Name == name);
    }
}