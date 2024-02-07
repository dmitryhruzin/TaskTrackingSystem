using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.ProjectStatuses;
using MediatR;

namespace BuisnessLogicLayer.Commands.ProjectStatuses.DeleteProjectStatus;

public class DeleteProjectStatusCommandHandler : IRequestHandler<DeleteProjectStatusRequest>
{
    private readonly IProjectStatusService _projectStatusService;

    public DeleteProjectStatusCommandHandler(IProjectStatusService projectStatusService)
    {
        _projectStatusService = projectStatusService;
    }
    
    public async Task Handle(DeleteProjectStatusRequest request, CancellationToken cancellationToken)
    {
        await _projectStatusService.DeleteAsync(request.Id, cancellationToken);
    }
}