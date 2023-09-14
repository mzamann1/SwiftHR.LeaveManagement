using AutoMapper;
using SwiftHR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using SwiftHR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using SwiftHR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using SwiftHR.LeaveManagement.Domain.Entities;

namespace SwiftHR.LeaveManagement.Application.MappingProfile;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocationDetailsDto, LeaveAllocation>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
    }
}