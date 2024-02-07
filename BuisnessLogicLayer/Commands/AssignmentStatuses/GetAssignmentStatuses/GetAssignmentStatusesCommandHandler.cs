using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.AssignmentStatuses;
using BuisnessLogicLayer.Responses.AssignmentStatuses;
using MediatR;

namespace BuisnessLogicLayer.Commands.AssignmentStatuses.GetAssignmentStatuses;

public class GetAssignmentStatusesCommandHandler : IRequestHandler<GetAssignmentStatusesRequest, GetAssignmentStatusesResponse>
{
    private readonly IMapper _mapper;
    private readonly IAssignmentStatusService _assignmentStatusService;

    public GetAssignmentStatusesCommandHandler(
        IMapper mapper,
        IAssignmentStatusService assignmentStatusService)
    {
        _mapper = mapper;
        _assignmentStatusService = assignmentStatusService;
    }
    
    public async Task<GetAssignmentStatusesResponse> Handle(GetAssignmentStatusesRequest request, CancellationToken cancellationToken)
    {
        var model = await _assignmentStatusService.GetAllAsync(cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetAssignmentStatusResponse>>(model));
    }
}