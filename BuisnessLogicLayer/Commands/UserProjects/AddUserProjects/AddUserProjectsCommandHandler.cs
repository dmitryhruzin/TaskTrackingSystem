using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.UserProjects;
using MediatR;

namespace BuisnessLogicLayer.Commands.UserProjects.AddUserProjects;

public class AddUserProjectsCommandHandler : IRequestHandler<AddUserProjectsRequest>
{
    private readonly IUserProjectService _userProjectService;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public AddUserProjectsCommandHandler(
        IUserProjectService userProjectService,
        IUserService userService,
        IEmailService emailService,
        IMapper mapper)
    {
        _userProjectService = userProjectService;
        _userService = userService;
        _emailService = emailService;
        _mapper = mapper;
    }
    
    public async Task Handle(AddUserProjectsRequest request, CancellationToken cancellationToken)
    {
        var models = _mapper.Map<IEnumerable<UserProject>>(request);

        await _userProjectService.AddUserProjectsAsync(models, cancellationToken);

        var ids = models.Select(m => m.UserId);

        var users = await _userService
            .GetAllByExpressionAsync(u => ids.Contains(u.Id), cancellationToken);
        
        var message = new MessageModel(
            users.Select(u => u.Email),
            "You have a new task!",
            $"Hello! We are to inform that you have been added to a new task!\n\n" +
            $"Please check it out! Maybe it's your moment!\n" +
            $"Thanks, The Task tracking system team.");

        await _emailService.SendEmailAsync(message);
    }
}