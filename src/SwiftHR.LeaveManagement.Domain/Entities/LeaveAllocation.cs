﻿using SwiftHR.LeaveManagement.Domain.Common;

namespace SwiftHR.LeaveManagement.Domain.Entities;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }
    public LeaveType? LeaveType { get; set; } = new();
    public int LeaveTypeId { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public int Period { get; set; }
}