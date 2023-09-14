using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwiftHR.LeaveManagement.Identity.Models;

namespace SwiftHR.LeaveManagement.Identity.Data;

public class SwiftHrIdentityDbContext : IdentityDbContext<AppUser>
{
    public SwiftHrIdentityDbContext(DbContextOptions<SwiftHrIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(SwiftHrIdentityDbContext).Assembly);
    }
}