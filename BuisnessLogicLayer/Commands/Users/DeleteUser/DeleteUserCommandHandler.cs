using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Users;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserRequest>
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public DeleteUserCommandHandler(
        IUserService userService,
        IEmailService emailService)
    {
        _userService = userService;
        _emailService = emailService;
    }
    
    public async Task Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var model = await _userService.GetByIdAsync(request.Id, cancellationToken);

        await _userService.DeleteAsync(request.Id, cancellationToken);
            
        var message = new MessageModel(
            model.Email,
            "Your account has been deleted.",
            $"Hello {model.UserName}! We have found out that your account has been deleted!\n\n" +
            $"That's so sad. Share your opinion with us and we will improve our service!\n" +
            $"Thanks, The Task tracking system team.");

        await _emailService.SendEmailAsync(message);
    }
}