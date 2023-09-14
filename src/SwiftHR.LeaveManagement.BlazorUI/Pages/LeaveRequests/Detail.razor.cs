using Microsoft.AspNetCore.Components;
using SwiftHR.LeaveManagement.BlazorUI.Interfaces;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveRequests;

namespace SwiftHR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Detail
{
    private string ClassName;
    private string HeadingText;
    [Inject] private ILeaveRequestService leaveRequestService { get; set; }
    [Inject] private NavigationManager navigationManager { get; set; }
    [Parameter] public int id { get; set; }

    public LeaveRequestVM Model { get; private set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        Model = await leaveRequestService.GetLeaveRequest(id);
    }

    protected override async Task OnInitializedAsync()
    {
        if (Model.Approved == null)
        {
            ClassName = "warning";
            HeadingText = "Pending Approval";
        }
        else if (Model.Approved == true)
        {
            ClassName = "success";
            HeadingText = "Approved";
        }
        else
        {
            ClassName = "danger";
            HeadingText = "Rejected";
        }
    }

    private async Task ChangeApproval(bool approvalStatus)
    {
        await leaveRequestService.ApproveLeaveRequest(id, approvalStatus);
        navigationManager.NavigateTo("/leaverequests/");
    }
}