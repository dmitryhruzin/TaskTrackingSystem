using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.UserProjects;
using MediatR;

namespace BuisnessLogicLayer.Commands.UserProjects.DeleteUserProjects;

public class DeleteUserProjectsCommandHandler : IRequestHandler<DeleteUserProjectsRequest>
{
    private readonly IUserProjectService _userProjectService;

    public DeleteUserProjectsCommandHandler(IUserProjectService userProjectService)
    {
        _userProjectService = userProjectService;
    }
    
    public async Task Handle(DeleteUserProjectsRequest request, CancellationToken cancellationToken)
    {
        await _userProjectService.DeleteUserProjectsAsync(request.UserProjects.Select(up => up.Id), cancellationToken);
    }
}