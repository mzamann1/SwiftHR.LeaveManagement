using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using SwiftHR.LeaveManagement.Persistence.Data;
using SwiftHR.LeaveManagement.Persistence.Repositories;

namespace SwiftHR.LeaveManagement.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SwiftHRDataContext>(option =>
            {
                option.UseNpgsql(configuration.GetConnectionString("SwiftHRConnectionString"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

            return services;
        }
    }
}