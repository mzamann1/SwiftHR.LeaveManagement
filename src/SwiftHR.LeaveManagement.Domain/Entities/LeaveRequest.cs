using System.ComponentModel.DataAnnotations.Schema;
using SwiftHR.LeaveManagement.Domain.Common;

namespace SwiftHR.LeaveManagement.Domain.Entities;

public class LeaveRequest : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [ForeignKey("LeaveTypeId")] public LeaveType LeaveType { get; set; }

    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool? Cancelled { get; set; }
    public string RequestingEmployeeId { get; set; } = string.Empty;
}