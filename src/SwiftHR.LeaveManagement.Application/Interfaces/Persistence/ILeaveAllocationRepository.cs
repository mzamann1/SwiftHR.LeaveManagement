using SwiftHR.LeaveManagement.Domain.Entities;

namespace SwiftHR.LeaveManagement.Application.Interfaces.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationsWithDetails(int id, CancellationToken cancellationToken);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(CancellationToken cancellationToken);

    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId,
        CancellationToken cancellationToken);

    Task<bool> AllocationExists(string userId, int leaveTypeId, int period, CancellationToken cancellationToken);
    Task AddAllocations(List<LeaveAllocation> allocation, CancellationToken cancellationToken);
    Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId, CancellationToken cancellationToken);
}