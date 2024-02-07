using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Assignments;
using BuisnessLogicLayer.Responses.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Commands.Assignments.GetAssignmentsByUserId;

public class GetAssignmentsByUserIdCommandHandler : IRequestHandler<GetAssignmentsByUserIdRequest, GetAssignmentsResponse>
{
    private readonly IMapper _mapper;
    private readonly IAssignmentService _assignmentService;

    public GetAssignmentsByUserIdCommandHandler(
        IMapper mapper,
        IAssignmentService assignmentService)
    {
        _mapper = mapper;
        _assignmentService = assignmentService;
    }
    
    public async Task<GetAssignmentsResponse> Handle(GetAssignmentsByUserIdRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<Assignment, bool>> expression = a => a.UserProjects.Any(up => up.UserId == request.UserId);

        var models = await _assignmentService.GetByExpressionAsync(expression, cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetAssignmentResponse>>(models));
    }
}