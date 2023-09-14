using Microsoft.AspNetCore.Components;
using SwiftHR.LeaveManagement.BlazorUI.Interfaces;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveTypes;

namespace SwiftHR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Create
{
    [Inject] private ILeaveTypeService leaveTypeService { get; set; }

    [Inject] private ILeaveRequestService leaveRequestService { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }
    private LeaveRequestVM LeaveRequest { get; } = new();
    private List<LeaveTypeVM> leaveTypeVMs { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        leaveTypeVMs = await leaveTypeService.GetLeaveTypes();
    }

    private async Task HandleValidSubmit()
    {
        await leaveRequestService.CreateLeaveRequest(LeaveRequest);
        NavigationManager.NavigateTo("/leaverequests/");
    }
}