using SwiftHR.LeaveManagement.Domain.Entities;

namespace SwiftHR.LeaveManagement.Application.Interfaces.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id, CancellationToken cancellationToken);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(CancellationToken cancellationToken);
    Task<List<LeaveAllocation>> GetLeaveAllocationsByUserIdWithDetails(string userId, CancellationToken cancellationToken);
    Task<bool> AllocationExists(string userId, int leaveTypeId, int period, CancellationToken cancellationToken);
    Task AddAllocation(List<LeaveAllocation> allocation, CancellationToken cancellationToken);
    Task<List<LeaveAllocation>> GetAllocationsByUserId(string userId, CancellationToken cancellationToken);

}