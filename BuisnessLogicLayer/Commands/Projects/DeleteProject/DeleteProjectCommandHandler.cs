using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Projects;
using MediatR;

namespace BuisnessLogicLayer.Commands.Projects.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectRequest>
{
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public DeleteProjectCommandHandler(
        IProjectService projectService,
        IUserService userService,
        IEmailService emailService)
    {
        _projectService = projectService;
        _userService = userService;
        _emailService = emailService;
    }
    
    public async Task Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> expression = u =>
            u.UserProjects.Any(up => up.Task.ProjectId == request.Id && up.Task.ManagerId != u.Id);

        var users = await _userService.GetAllByExpressionAsync(expression, cancellationToken);

        var project = await _projectService.GetByIdAsync(request.Id, cancellationToken);
        
        await _projectService.DeleteAsync(request.Id, cancellationToken);

        if (users.Any())
        {
            var message = new MessageModel(
                users.Select(t => t.Email),
                $"Project {project.Name} has been deleted.",
                $"Hello! We have noticed that the project {project.Name} has been deleted!\n\n" +
                $" We hope it was comfortable to work with us!\n" +
                $"Thanks, The Task tracking system team.");

            await _emailService.SendEmailAsync(message);
        }
    }
}