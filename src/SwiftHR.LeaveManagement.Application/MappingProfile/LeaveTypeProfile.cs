using AutoMapper;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using SwiftHR.LeaveManagement.Domain.Entities;

namespace SwiftHR.LeaveManagement.Application.MappingProfile;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
        CreateMap<LeaveType, LeaveTypeDetailDto>();
        CreateMap<CreateLeaveTypeCommand, LeaveType>();
        CreateMap<UpdateLeaveTypeCommand, LeaveType>();
    }
}