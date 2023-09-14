using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SwiftHR.LeaveManagement.BlazorUI.Interfaces;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveRequests;

namespace SwiftHR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class EmployeeIndex
{
    [Inject] private ILeaveRequestService leaveRequestService { get; set; }
    [Inject] private IJSRuntime js { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    public EmployeeLeaveRequestViewVM Model { get; set; } = new();
    public string Message { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        Model = await leaveRequestService.GetUserLeaveRequests();
    }

    private async Task CancelRequestAsync(int id)
    {
        var confirm = await js.InvokeAsync<bool>("confirm", "Do you want to cancel this request?");
        if (confirm)
        {
            var response = await leaveRequestService.CancelLeaveRequest(id);
            if (response.Success)
                StateHasChanged();
            else
                Message = response.Message;
        }
    }
}