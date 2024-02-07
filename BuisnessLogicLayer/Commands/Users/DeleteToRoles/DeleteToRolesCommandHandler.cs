using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Users;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.DeleteToRoles;

public class DeleteToRolesCommandHandler : IRequestHandler<DeleteToRolesRequest>
{
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public DeleteToRolesCommandHandler(
        IRoleService roleService,
        IUserService userService,
        IEmailService emailService)
    {
        _roleService = roleService;
        _userService = userService;
        _emailService = emailService;
    }
    
    public async Task Handle(DeleteToRolesRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<Role, bool>> expression = r => request.RoleIds.Contains(r.Id);

        var roles = await _roleService.GetAllByExpressionAsync(expression, cancellationToken);

        var user = await _userService.GetByIdAsync(request.Id, cancellationToken);

        foreach (var role in roles)
        {
            user.Roles.Remove(role);
        }

        await _userService.UpdateAsync(user, cancellationToken);
        
        var strRoles = string.Join(", ", roles.Select(r => r.Name));
        
        var message = new MessageModel(
            user.Email,
            "You have been excluded from the roles.",
            $"Hello {user.UserName}! You have been excluded from the roles: {strRoles}!\n\n" +
            $"We hope this will better for you! Keep going to achieve new goals.\n" +
            $"Thanks, The Task tracking system team.");

        await _emailService.SendEmailAsync(message);
    }
}