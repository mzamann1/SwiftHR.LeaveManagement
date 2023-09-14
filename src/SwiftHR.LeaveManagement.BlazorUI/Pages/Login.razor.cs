using Microsoft.AspNetCore.Components;
using SwiftHR.LeaveManagement.BlazorUI.Interfaces;
using SwiftHR.LeaveManagement.BlazorUI.Models;

namespace SwiftHR.LeaveManagement.BlazorUI.Pages;

public partial class Login
{
    public LoginVM Model { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    public string Message { get; set; }

    [Inject] private IAuthenticationService AuthenticationService { get; set; }

    protected override void OnInitialized()
    {
        Model = new LoginVM();
    }

    protected async Task HandleLogin()
    {
        Console.WriteLine("HandleLogin method called");

        if (await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
            NavigationManager.NavigateTo("/");
        Message = "Username/password combination unknown";
        Console.WriteLine("Authentication failed");
    }
}