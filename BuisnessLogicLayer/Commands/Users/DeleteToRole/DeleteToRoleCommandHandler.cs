using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Users;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.DeleteToRole;

public class DeleteToRoleCommandHandler : IRequestHandler<DeleteToRoleRequest>
{
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public DeleteToRoleCommandHandler(
        IRoleService roleService,
        IUserService userService,
        IEmailService emailService)
    {
        _roleService = roleService;
        _userService = userService;
        _emailService = emailService;
    }
    
    public async Task Handle(DeleteToRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleService.GetByIdAsync(request.RoleId, cancellationToken);

        var user = await _userService.GetByIdAsync(request.Id, cancellationToken);

        user.Roles.Remove(role);

        await _userService.UpdateAsync(user, cancellationToken);
        
        var message = new MessageModel(
            user.Email,
            "You have been excluded from the role.",
            $"Hello {user.UserName}! You have been excluded from the role {role.Name}!\n\n" +
            $"We hope this will better for you! Keep going to achieve new goals.\n" +
            $"Thanks, The Task tracking system team.");

        await _emailService.SendEmailAsync(message);
    }
}