using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Users;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserRequest>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly IValidator<UpdateUserRequest> _validator;

    public UpdateUserCommandHandler(
        IMapper mapper,
        IUserService userService,
        IEmailService emailService,
        IValidator<UpdateUserRequest> validator)
    {
        _mapper = mapper;
        _userService = userService;
        _emailService = emailService;
        _validator = validator;
    }
    
    public async Task Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<User>(request);

        await _userService.UpdateAsync(model, cancellationToken);
        
        var message = new MessageModel(
            model.Email,
            "You have changed your personal information.",
            $"Hello {model.UserName}! We have noticed that you changed personal info!\n\n" +
            $"This is great! Hope it will be easier next time.\n" +
            $"Thanks, The Task tracking system team.");

        await _emailService.SendEmailAsync(message);
    }
}