using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Assignments;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Assignments.UpdateAssignment;

public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentRequest>
{
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateAssignmentRequest> _validator;
    private readonly IAssignmentService _assignmentService;
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;

    public UpdateAssignmentCommandHandler(
        IMapper mapper,
        IValidator<UpdateAssignmentRequest> validator,
        IAssignmentService assignmentService,
        IEmailService emailService,
        IUserService userService)
    {
        _mapper = mapper;
        _validator = validator;
        _assignmentService = assignmentService;
        _emailService = emailService;
        _userService = userService;
    }
    
    public async Task Handle(UpdateAssignmentRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<Assignment>(request);

        await _assignmentService.UpdateAsync(model, cancellationToken);
        
        Expression<Func<User, bool>> expression = u =>
            u.UserProjects.Any(up => up.TaskId == request.Id && up.Task.ManagerId != u.Id);

        var users = await _userService.GetAllByExpressionAsync(expression, cancellationToken);

        if (users.Any())
        {
            var message = new MessageModel(
                users.Select(t => t.Email),
                $"Your task has been updated!",
                $"Hello! We are to inform that {model.Name} has been updated!\n\n" +
                $"Please check out what's new. Maybe this will help you to make your assignment better!\n" +
                $"Thanks, The Task tracking system team.");

            await _emailService.SendEmailAsync(message);
        }
    }
}