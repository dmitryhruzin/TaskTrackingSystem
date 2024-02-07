using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Users;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.AddToRoles;

public class AddToRolesCommandHandler : IRequestHandler<AddToRolesRequest>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public AddToRolesCommandHandler(
        IMapper mapper,
        IUserService userService,
        IEmailService emailService)
    {
        _mapper = mapper;
        _userService = userService;
        _emailService = emailService;
    }
    
    public async Task Handle(AddToRolesRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(request.Id, cancellationToken);

        var roles = _mapper.Map<IReadOnlyCollection<Role>>(request.Roles);

        foreach (var role in roles)
        {
            user.Roles.Add(role);
        }

        await _userService.UpdateAsync(user, cancellationToken);
        
        var strRoles = string.Join(", ", roles.Select(r => r.Name));
        
        var message = new MessageModel(user.Email,
            "Your opportunities and responsibilities have expanded.",
            $"Hello {user.UserName}! You have been added to new roles: {strRoles}!\n\n" +
            $"Great one! But don't stop! New heights are waiting!\n" +
            $"Thanks, The Task tracking system team.");

        await _emailService.SendEmailAsync(message);
    }
}