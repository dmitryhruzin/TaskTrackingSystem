using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Assignments;
using BuisnessLogicLayer.Responses.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Commands.Assignments.GetAssignment;

public class GetAssignmentCommandHandler : IRequestHandler<GetAssignmentRequest, GetAssignmentResponse>
{
    private readonly IMapper _mapper;
    private readonly IAssignmentService _assignmentService;

    public GetAssignmentCommandHandler(
        IMapper mapper,
        IAssignmentService assignmentService)
    {
        _mapper = mapper;
        _assignmentService = assignmentService;
    }
    
    public async Task<GetAssignmentResponse> Handle(GetAssignmentRequest request, CancellationToken cancellationToken)
    {
        var model = await _assignmentService.GetByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<GetAssignmentResponse>(model);
    }
}