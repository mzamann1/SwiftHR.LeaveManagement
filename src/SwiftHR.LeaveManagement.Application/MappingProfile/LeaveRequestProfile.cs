using AutoMapper;
using SwiftHR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using SwiftHR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using SwiftHR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using SwiftHR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using SwiftHR.LeaveManagement.Domain.Entities;

namespace SwiftHR.LeaveManagement.Application.MappingProfile;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestListDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequestDetailsDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
        CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
    }
}