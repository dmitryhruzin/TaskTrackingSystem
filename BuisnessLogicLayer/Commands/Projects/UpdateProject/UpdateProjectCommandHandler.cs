using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Projects;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Projects.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectRequest>
{
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly IValidator<UpdateProjectRequest> _validator;
    private readonly IMapper _mapper;

    public UpdateProjectCommandHandler(
        IProjectService projectService,
        IUserService userService,
        IEmailService emailService,
        IValidator<UpdateProjectRequest> validator,
        IMapper mapper)
    {
        _projectService = projectService;
        _userService = userService;
        _emailService = emailService;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        Expression<Func<User, bool>> expression = u =>
            u.UserProjects.Any(up => up.Task.ProjectId == request.Id && up.Task.ManagerId != u.Id);

        var users = await _userService.GetAllByExpressionAsync(expression, cancellationToken);

        var model = _mapper.Map<Project>(request);

        await _projectService.UpdateAsync(model, cancellationToken);
        
        if (users.Any())
        {
            var message = new MessageModel(
                users.Select(t => t.Email),
                $"Project {model.Name} has been modified!.",
                $"Hello! We are happy to announce that the project {model.Name} has been modified!\n\n" +
                $"Please, check what's new! This will help you to become better!\n" +
                $"Small win today--big achievement tomorrow!\n" +
                $"Thanks,The Task tracking system team.");

            await _emailService.SendEmailAsync(message);
        }
    }
}