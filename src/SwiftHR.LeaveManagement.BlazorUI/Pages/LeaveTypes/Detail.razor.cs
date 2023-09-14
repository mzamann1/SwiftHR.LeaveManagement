using Microsoft.AspNetCore.Components;
using SwiftHR.LeaveManagement.BlazorUI.Interfaces;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveTypes;

namespace SwiftHR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Detail
{
    private LeaveTypeVM leaveType = new();

    [Inject] private ILeaveTypeService _client { get; set; }

    [Parameter] public int id { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        leaveType = await _client.GetLeaveTypeDetails(id);
    }
}