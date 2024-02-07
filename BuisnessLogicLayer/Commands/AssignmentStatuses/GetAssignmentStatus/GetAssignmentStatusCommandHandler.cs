using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.AssignmentStatuses;
using BuisnessLogicLayer.Responses.AssignmentStatuses;
using MediatR;

namespace BuisnessLogicLayer.Commands.AssignmentStatuses.GetAssignmentStatus;

public class GetAssignmentStatusCommandHandler : IRequestHandler<GetAssignmentStatusRequest, GetAssignmentStatusResponse>
{
    private readonly IMapper _mapper;
    private readonly IAssignmentStatusService _assignmentStatusService;

    public GetAssignmentStatusCommandHandler(
        IMapper mapper,
        IAssignmentStatusService assignmentStatusService)
    {
        _mapper = mapper;
        _assignmentStatusService = assignmentStatusService;
    }
    
    public async Task<GetAssignmentStatusResponse> Handle(GetAssignmentStatusRequest request, CancellationToken cancellationToken)
    {
        var model = await _assignmentStatusService.GetByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<GetAssignmentStatusResponse>(model);
    }
}