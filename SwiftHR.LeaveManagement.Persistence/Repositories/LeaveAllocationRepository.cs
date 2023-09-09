using Microsoft.EntityFrameworkCore;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using SwiftHR.LeaveManagement.Domain.Entities;
using SwiftHR.LeaveManagement.Persistence.Data;

namespace SwiftHR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(SwiftHRDataContext context) : base(context)
        {
        }

        public async Task AddAllocation(List<LeaveAllocation> allocation, CancellationToken cancellationToken)
        {
            await _context.AddRangeAsync(allocation, cancellationToken);
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period, CancellationToken cancellationToken)
        {
            return await _context.LeaveAllocations.AnyAsync(la => la.EmployeeId == userId
                                                                    && la.LeaveTypeId == leaveTypeId
                                                                    && la.Period == period,
                                                                    cancellationToken
             );
        }

        public async Task<List<LeaveAllocation>> GetAllocationsByUserId(string userId, CancellationToken cancellationToken)
        {
            return await _context.LeaveAllocations
               .Where(la => la.EmployeeId == userId)
               .ToListAsync(cancellationToken);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsByUserIdWithDetails(string userId, CancellationToken cancellationToken)
        {
            return await _context.LeaveAllocations
                .Where(la => la.EmployeeId == userId)
                .Include(la => la.LeaveType)
                .ToListAsync(cancellationToken);

        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(CancellationToken cancellationToken)
        {
            return await _context.LeaveAllocations
                .Include(la => la.LeaveType)
                .ToListAsync(cancellationToken);
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id, CancellationToken cancellationToken)
        {
            return await _context.LeaveAllocations
               .Include(la => la.LeaveType)
               .SingleOrDefaultAsync(la => la.Id == id, cancellationToken);
        }
    }



}
