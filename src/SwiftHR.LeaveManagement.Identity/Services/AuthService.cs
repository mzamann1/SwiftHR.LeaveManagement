using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SwiftHR.LeaveManagement.Application.Exceptions;
using SwiftHR.LeaveManagement.Application.Interfaces.Identity;
using SwiftHR.LeaveManagement.Application.Models.Identity;
using SwiftHR.LeaveManagement.Identity.Models;

namespace SwiftHR.LeaveManagement.Identity.Services;

public class AuthService : IAuthService
{
    private readonly IOptions<JwtSettings> _jwtOptions;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AuthService(UserManager<AppUser> userManager, IOptions<JwtSettings> jwtOptions,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions;
        _signInManager = signInManager;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundException($"User with {request.Email} not found", request.Email);


        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
            throw new BadRequestException($"Credentials for '{request.Email}' are not valid");

        var jwtSecurityToken = await GenerateToken(user);

        return new AuthResponse
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
        };
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var user = new AppUser
        {
            Email = request.Email,
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var str = new StringBuilder();

            foreach (var err in result.Errors) str.Append($"*{err.Description}\n");
            throw new BadRequestException($"{str}");
        }

        await _userManager.AddToRoleAsync(user, "Employee");
        return new RegistrationResponse { UserId = user.Id };
    }

    private async Task<JwtSecurityToken> GenerateToken(AppUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);

        var roles = await _userManager.GetRolesAsync(user);


        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToString();


        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("uid", user.Id)
        };


        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtOptions.Value.Issuer,
            _jwtOptions.Value.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.Value.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}