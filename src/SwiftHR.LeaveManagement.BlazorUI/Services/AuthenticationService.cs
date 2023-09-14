using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SwiftHR.LeaveManagement.BlazorUI.Interfaces;
using SwiftHR.LeaveManagement.BlazorUI.Providers;
using SwiftHR.LeaveManagement.BlazorUI.Services.Base;

namespace SwiftHR.LeaveManagement.BlazorUI.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(IClient client,
        ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationStateProvider) : base(client, localStorage)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            Console.WriteLine("Authenticating user with email: " + email);

            var authenticationRequest = new AuthRequest { Email = email, Password = password };
            var authenticationResponse = await _client.LoginAsync(authenticationRequest);
            if (authenticationResponse.Token != string.Empty)
            {
                await _localStorage.SetItemAsync("token", authenticationResponse.Token);

                // Set claims in Blazor and login state
                await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

                var token = await _localStorage.GetItemAsync<string>("token");
                Console.WriteLine("Token saved in local storage: " + token);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred in AuthenticateAsync: {0}", ex);
            return false;
        }
    }

    public async Task Logout()
    {
        Console.WriteLine("Logging out user");

        // remove claims in Blazor and invalidate login state
        await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
    }

    public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email,
        string password)
    {
        Console.WriteLine("Registering user with email: " + email);

        var registrationRequest = new RegistrationRequest
            { FirstName = firstName, LastName = lastName, Email = email, UserName = userName, Password = password };
        var response = await _client.RegisterAsync(registrationRequest);

        if (!string.IsNullOrEmpty(response.UserId)) return true;
        return false;
    }
}