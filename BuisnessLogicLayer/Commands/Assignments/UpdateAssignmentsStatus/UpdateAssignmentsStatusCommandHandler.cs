using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Commands.Assignments.UpdateAssignmentsStatus;

public class UpdateAssignmentsStatusCommandHandler : IRequestHandler<UpdateAssignmentsStatusRequest>
{
    private readonly IMapper _mapper;
    private readonly IAssignmentService _assignmentService;

    public UpdateAssignmentsStatusCommandHandler(
        IMapper mapper,
        IAssignmentService assignmentService)
    {
        _mapper = mapper;
        _assignmentService = assignmentService;
    }
    
    public async Task Handle(UpdateAssignmentsStatusRequest request, CancellationToken cancellationToken)
    {
        var status = _mapper.Map<AssignmentStatus>(request.Status);

        var model = await _assignmentService.GetByIdAsync(request.Id, cancellationToken);

        model.Status = status;

        await _assignmentService.UpdateAsync(model, cancellationToken);
    }
}