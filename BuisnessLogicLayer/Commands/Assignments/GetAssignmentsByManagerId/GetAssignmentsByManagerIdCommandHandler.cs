using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Assignments;
using BuisnessLogicLayer.Responses.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Commands.Assignments.GetAssignmentsByManagerId;

public class GetAssignmentsByManagerIdCommandHandler : IRequestHandler<GetAssignmentsByManagerIdRequest, GetAssignmentsResponse>
{
    private readonly IMapper _mapper;
    private readonly IAssignmentService _assignmentService;

    public GetAssignmentsByManagerIdCommandHandler(
        IMapper mapper,
        IAssignmentService assignmentService)
    {
        _mapper = mapper;
        _assignmentService = assignmentService;
    }
    
    public async Task<GetAssignmentsResponse> Handle(GetAssignmentsByManagerIdRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<Assignment, bool>> expression = a => a.ManagerId == request.ManagerId;

        var models = await _assignmentService.GetByExpressionAsync(expression, cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetAssignmentResponse>>(models));
    }
}