using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Commands.Assignments.DeleteAssignment;

public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentRequest>
{
    private readonly IAssignmentService _assignmentService;
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;

    public DeleteAssignmentCommandHandler(
        IAssignmentService assignmentService,
        IEmailService emailService,
        IUserService userService)
    {
        _assignmentService = assignmentService;
        _emailService = emailService;
        _userService = userService;
    }
    
    public async Task Handle(DeleteAssignmentRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> expression = u =>
            u.UserProjects.Any(up => up.TaskId == request.Id && up.Task.ManagerId != u.Id);

        var users = await _userService.GetAllByExpressionAsync(expression, cancellationToken);

        var assignment = await _assignmentService.GetByIdAsync(request.Id, cancellationToken);

        await _assignmentService.DeleteAsync(request.Id, cancellationToken);

        if (users.Any())
        {
            var message = new MessageModel(
                users.Select(t => t.Email),
                $"Task you have been working has been deleted!",
                $"Hello! We are to inform that {assignment.Name} has been deleted!\n\n" +
                $"Please, check out what's new. Maybe, it's time to relax?\n" +
                $"Tip: the best way is to change your activity.\n" +
                $"Thanks, The Task tracking system team.");

            await _emailService.SendEmailAsync(message);
        }
    }
}