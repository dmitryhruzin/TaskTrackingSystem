using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Assignments;
using BuisnessLogicLayer.Responses.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Commands.Assignments.GetAssignments;

public class GetAssignmentsCommandHandler : IRequestHandler<GetAssignmentsRequest, GetAssignmentsResponse>
{
    private readonly IMapper _mapper;
    private readonly IAssignmentService _assignmentService;

    public GetAssignmentsCommandHandler(
        IMapper mapper,
        IAssignmentService assignmentService)
    {
        _mapper = mapper;
        _assignmentService = assignmentService;
    }
    
    public async Task<GetAssignmentsResponse> Handle(GetAssignmentsRequest request, CancellationToken cancellationToken)
    {
        var models = await _assignmentService.GetAllAsync(cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetAssignmentResponse>>(models));
    }
}