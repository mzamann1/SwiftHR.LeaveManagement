using Microsoft.EntityFrameworkCore;
using SwiftHR.LeaveManagement.Domain.Common;
using SwiftHR.LeaveManagement.Domain.Entities;

namespace SwiftHR.LeaveManagement.Persistence.Data;

public class SwiftHRDataContext : DbContext
{
    public SwiftHRDataContext(DbContextOptions<SwiftHRDataContext> options) : base(options)
    {
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SwiftHRDataContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            if (entry.State == EntityState.Modified) entry.Entity.DateModified = DateTime.UtcNow;

            if (entry.State == EntityState.Added) entry.Entity.DateCreated = DateTime.UtcNow;

            if (entry.State == EntityState.Deleted)
            {
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedDate = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}