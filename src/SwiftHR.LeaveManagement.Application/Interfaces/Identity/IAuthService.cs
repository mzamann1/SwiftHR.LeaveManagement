using SwiftHR.LeaveManagement.Application.Models.Identity;

namespace SwiftHR.LeaveManagement.Application.Interfaces.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest response);
    Task<RegistrationResponse> Register(RegistrationRequest response);
}