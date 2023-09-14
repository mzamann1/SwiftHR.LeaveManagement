using System.ComponentModel.DataAnnotations;

namespace SwiftHR.LeaveManagement.BlazorUI.Models.LeaveTypes;

public class LeaveTypeVM
{
    public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] [Display(Name = "Number")] public int DefaultDays { get; set; }
}