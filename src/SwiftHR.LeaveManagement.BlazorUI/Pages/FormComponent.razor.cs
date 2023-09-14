using Microsoft.AspNetCore.Components;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveTypes;

namespace SwiftHR.LeaveManagement.BlazorUI.Pages;

public partial class FormComponent
{
    [Parameter] public bool Disabled { get; set; } = false;
    [Parameter] public LeaveTypeVM LeaveType { get; set; }
    [Parameter] public string ButtonText { get; set; } = "Save";
    [Parameter] public EventCallback OnValidSubmit { get; set; }
}