using Microsoft.EntityFrameworkCore;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using SwiftHR.LeaveManagement.Domain.Entities;
using SwiftHR.LeaveManagement.Persistence.Data;

namespace SwiftHR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(SwiftHRDataContext context) : base(context)
    {
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations, CancellationToken cancellationToken)
    {
        await _context.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period,
        CancellationToken cancellationToken)
    {
        return await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
                                                             && q.LeaveTypeId == leaveTypeId && q.Period == period);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(CancellationToken cancellationToken)
    {
        var leaveAllocations = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId,
        CancellationToken cancellationToken)
    {
        var leaveAllocations = await _context.LeaveAllocations.Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType).ToListAsync();
        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId,
        CancellationToken cancellationToken)
    {
        var leaveAllocations = await _context.LeaveAllocations
            .FirstOrDefaultAsync(q => q.EmployeeId == userId
                                      && q.LeaveTypeId == leaveTypeId);
        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationsWithDetails(int id, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);

        return leaveAllocation;
    }
}