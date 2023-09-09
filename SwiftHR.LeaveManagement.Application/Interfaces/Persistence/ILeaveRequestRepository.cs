using SwiftHR.LeaveManagement.Domain.Entities;

namespace SwiftHR.LeaveManagement.Application.Interfaces.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id, CancellationToken cancellationToken);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(CancellationToken cancellationToken);
    Task<List<LeaveRequest>> GetLeaveRequestByUserIdWithDetailsAsync(string userId, CancellationToken cancellationToken);
}