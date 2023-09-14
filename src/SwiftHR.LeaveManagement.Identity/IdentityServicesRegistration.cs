using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SwiftHR.LeaveManagement.Application.Interfaces.Identity;
using SwiftHR.LeaveManagement.Application.Models.Identity;
using SwiftHR.LeaveManagement.Identity.Data;
using SwiftHR.LeaveManagement.Identity.Models;
using SwiftHR.LeaveManagement.Identity.Services;

namespace SwiftHR.LeaveManagement.Identity;

public static class IdentityServicesRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddDbContext<SwiftHrIdentityDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SwiftHRConnectionString")));

        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<SwiftHrIdentityDbContext>()
            .AddDefaultTokenProviders();


        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserService, UserService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };
        });

        return services;
    }
}