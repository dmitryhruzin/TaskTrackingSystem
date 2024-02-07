using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Users;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.AddToRole;

public class AddToRoleCommandHandler : IRequestHandler<AddToRoleRequest>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public AddToRoleCommandHandler(
        IMapper mapper,
        IUserService userService,
        IEmailService emailService)
    {
        _mapper = mapper;
        _userService = userService;
        _emailService = emailService;
    }
    
    public async Task Handle(AddToRoleRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(request.Id, cancellationToken);

        var role = _mapper.Map<Role>(request.Role);
        
        user.Roles.Add(role);

        await _userService.UpdateAsync(user, cancellationToken);
        
        var message = new MessageModel(
            user.Email,
            "Your opportunities and responsibilities have expanded.",
            $"Hello {user.UserName}! You have been added a new role!\n\n" +
            $"Our congratulations. We strongly believe that you will be the best {role.Name}.\n" +
            $"Thanks, The Task tracking system team.");

        await _emailService.SendEmailAsync(message);
    }
}