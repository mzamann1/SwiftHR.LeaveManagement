using Microsoft.EntityFrameworkCore;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using SwiftHR.LeaveManagement.Domain.Entities;
using SwiftHR.LeaveManagement.Persistence.Data;

namespace SwiftHR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(SwiftHRDataContext context) : base(context)
        {

        }

        public async Task<List<LeaveRequest>> GetLeaveRequestByUserIdWithDetailsAsync(string userId, CancellationToken cancellationToken)
        {
            var leaveRequests = await _context.LeaveRequests
                                               .Where(lr => lr.RequestingEmployeeId == userId)
                                              .Include(lr => lr.LeaveType)
                                              .ToListAsync(cancellationToken);
            return leaveRequests;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(CancellationToken cancellationToken)
        {
            var leaveRequests = await _context.LeaveRequests
                                              .Include(lr => lr.LeaveType)
                                              .ToListAsync(cancellationToken);
            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id, CancellationToken cancellationToken)
        {
            var leaveRequest = await _context.LeaveRequests
                                              .Include(lr => lr.LeaveType)
                                              .SingleOrDefaultAsync(lr => lr.Id == id, cancellationToken);
            return leaveRequest;
        }
    }



}
