using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Assignments;
using BuisnessLogicLayer.Responses.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Commands.Assignments.GetAssignmentsByProjectId;

public class GetAssignmentsByProjectIdCommandHandler : IRequestHandler<GetAssignmentsByProjectIdRequest, GetAssignmentsResponse>
{
    private readonly IMapper _mapper;
    private readonly IAssignmentService _assignmentService;

    public GetAssignmentsByProjectIdCommandHandler(
        IMapper mapper,
        IAssignmentService assignmentService)
    {
        _mapper = mapper;
        _assignmentService = assignmentService;
    }
    
    public async Task<GetAssignmentsResponse> Handle(GetAssignmentsByProjectIdRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<Assignment, bool>> expression = a => a.ProjectId == request.ProjectId;

        var models = await _assignmentService.GetByExpressionAsync(expression, cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetAssignmentResponse>>(models));
    }
}