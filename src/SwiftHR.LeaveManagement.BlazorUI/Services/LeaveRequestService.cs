﻿using AutoMapper;
using Blazored.LocalStorage;
using SwiftHR.LeaveManagement.BlazorUI.Interfaces;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveAllocations;
using SwiftHR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using SwiftHR.LeaveManagement.BlazorUI.Services.Base;

namespace SwiftHR.LeaveManagement.BlazorUI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    private readonly IMapper _mapper;

    public LeaveRequestService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client,
        localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<Response<Guid>> ApproveLeaveRequest(int id, bool approved)
    {
        var response = new Response<Guid>();
        var request = new ChangeLeaveRequestApprovalCommand { Approved = approved, Id = id };
        await _client.UpdateApprovalAsync(request);
        return response;
    }

    public async Task<Response<Guid>> CancelLeaveRequest(int id)
    {
        try
        {
            var response = new Response<Guid>();
            var request = new CancelLeaveRequestCommand { Id = id };
            await _client.CancelRequestAsync(request);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest)
    {
        try
        {
            var response = new Response<Guid>();
            var createLeaveRequest = _mapper.Map<CreateLeaveRequestCommand>(leaveRequest);

            await _client.LeaveRequestsPOSTAsync(createLeaveRequest);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public Task<Response<Guid>> DeleteLeaveRequest(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
    {
        var leaveRequests = await _client.LeaveRequestsAllAsync(false);

        var model = new AdminLeaveRequestViewVM
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
            PendingRequests = leaveRequests.Count(q => q.Approved == null),
            RejectedRequests = leaveRequests.Count(q => q.Approved == false),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };
        return model;
    }

    public async Task<LeaveRequestVM> GetLeaveRequest(int id)
    {
        await AddBearerToken();
        var leaveRequest = await _client.LeaveRequestsGETAsync(id);
        return _mapper.Map<LeaveRequestVM>(leaveRequest);
    }

    public async Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests()
    {
        await AddBearerToken();
        var leaveRequests = await _client.LeaveRequestsAllAsync(default);
        var allocations = await _client.LeaveAllocationsAllAsync(default);
        var model = new EmployeeLeaveRequestViewVM
        {
            LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocations),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
    }
}