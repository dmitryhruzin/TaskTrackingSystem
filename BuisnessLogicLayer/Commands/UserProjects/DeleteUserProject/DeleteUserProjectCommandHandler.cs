using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.UserProjects;
using MediatR;

namespace BuisnessLogicLayer.Commands.UserProjects.DeleteUserProject;

public class DeleteUserProjectCommandHandler : IRequestHandler<DeleteUserProjectRequest>
{
    private readonly IUserProjectService _userProjectService;
    private readonly IEmailService _emailService;

    public DeleteUserProjectCommandHandler(
        IUserProjectService userProjectService,
        IEmailService emailService)
    {
        _userProjectService = userProjectService;
        _emailService = emailService;
    }
    
    public async Task Handle(DeleteUserProjectRequest request, CancellationToken cancellationToken)
    {
        var userProject = await _userProjectService.GetByIdAsync(request.Id, cancellationToken);

        var email = userProject.User.Email;
        var taskName = userProject.Task.Name;

        await _userProjectService.DeleteAsync(userProject.Id, cancellationToken);

        var message = new MessageModel(
            userProject.User.Email,
            $"Hello! You have been excluded from the task {userProject.Task.Name}.",
            $"Hello! We have found out that you have been excluded from the task!\n\n" +
            $"May be it's time to upgrade yourself? We wish to to get back as soon as possible!\n" +
            $"Thanks, The Task tracking system team.");

        await _emailService.SendEmailAsync(message);
    }
}