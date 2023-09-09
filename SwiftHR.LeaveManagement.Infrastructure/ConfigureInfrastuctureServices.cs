﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwiftHR.LeaveManagement.Application.Interfaces.Email;
using SwiftHR.LeaveManagement.Application.Models.Email;
using SwiftHR.LeaveManagement.Infrastructure.EmailService;

namespace SwiftHR.LeaveManagement.Infrastructure
{
    public static class ConfigureInfrastuctureServices
    {
        public static IServiceCollection ConfigureInfrastureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            return services;
        }
    }
}