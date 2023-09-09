using SwiftHR.LeaveManagement.Domain.Common;

namespace SwiftHR.LeaveManagement.Domain.Entities;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}