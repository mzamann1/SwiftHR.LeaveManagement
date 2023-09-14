using Microsoft.AspNetCore.Components;
using SwiftHR.LeaveManagement.BlazorUI.Interfaces;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveRequests;

namespace SwiftHR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Index
{
    [Inject] private ILeaveRequestService leaveRequestService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    public AdminLeaveRequestViewVM Model { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Model = await leaveRequestService.GetAdminLeaveRequestList();
    }

    private void GoToDetails(int id)
    {
        NavigationManager.NavigateTo($"leaverequests/details/{id}");
    }
}