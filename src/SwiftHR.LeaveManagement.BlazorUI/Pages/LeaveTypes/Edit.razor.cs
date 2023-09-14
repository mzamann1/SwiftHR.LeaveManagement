using Microsoft.AspNetCore.Components;
using SwiftHR.LeaveManagement.BlazorUI.Interfaces;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveTypes;

namespace SwiftHR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Edit
{
    private LeaveTypeVM leaveType = new();

    [Inject] private ILeaveTypeService _client { get; set; }

    [Inject] private NavigationManager _navManager { get; set; }

    [Parameter] public int id { get; set; }

    public string Message { get; private set; }

    protected override async Task OnParametersSetAsync()
    {
        leaveType = await _client.GetLeaveTypeDetails(id);
    }

    private async Task EditLeaveType()
    {
        var response = await _client.UpdateLeaveType(id, leaveType);
        if (response.Success) _navManager.NavigateTo("/leavetypes/");
        Message = response.Message;
    }
}