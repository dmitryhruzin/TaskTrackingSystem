using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.UserProjects;
using MediatR;

namespace BuisnessLogicLayer.Commands.UserProjects.AddUserProject;

public class AddUserProjectCommandHandle: IRequestHandler<AddUserProjectRequest>
{
    private readonly IUserProjectService _userProjectService;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public AddUserProjectCommandHandle(
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
    
    public async Task Handle(AddUserProjectRequest request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<UserProject>(request);

        await _userProjectService.AddAsync(model, cancellationToken);

        var user = await _userService.GetByIdAsync(model.UserId, cancellationToken);
        
        var message = new MessageModel(
            user.Email,
            "You have a new task!",
            $"Hello! We are to inform that you have been added to a new task!\n\n" +
            $"Please check it out! Maybe it's your moment!\n" +
            $"Thanks, The Task tracking system team.");

        await _emailService.SendEmailAsync(message);
    }
}