using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.AssignmentStatuses;
using MediatR;

namespace BuisnessLogicLayer.Commands.AssignmentStatuses.DeleteAssignmentStatus;

public class DeleteAssignmentStatusCommandHandler : IRequestHandler<DeleteAssignmentStatusRequest>
{
    private readonly IAssignmentStatusService _assignmentStatusService;

    public DeleteAssignmentStatusCommandHandler(IAssignmentStatusService assignmentStatusService)
    {
        _assignmentStatusService = assignmentStatusService;
    }
    
    public async Task Handle(DeleteAssignmentStatusRequest request, CancellationToken cancellationToken)
    {
        await _assignmentStatusService.DeleteAsync(request.Id, cancellationToken);
    }
}