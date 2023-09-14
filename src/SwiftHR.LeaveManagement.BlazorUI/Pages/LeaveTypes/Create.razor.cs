using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using SwiftHR.LeaveManagement.BlazorUI.Interfaces;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveTypes;

namespace SwiftHR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Create
{
    private readonly LeaveTypeVM leaveType = new();

    [Inject] private NavigationManager _navManager { get; set; }

    [Inject] private ILeaveTypeService _client { get; set; }

    [Inject] private IToastService toastService { get; set; }

    public string Message { get; private set; }

    private async Task CreateLeaveType()
    {
        var response = await _client.CreateLeaveType(leaveType);
        if (response.Success)
        {
            toastService.ShowSuccess("Leave Type Created Successfully");
            _navManager.NavigateTo("/leavetypes/");
        }

        Message = response.Message;
    }
}