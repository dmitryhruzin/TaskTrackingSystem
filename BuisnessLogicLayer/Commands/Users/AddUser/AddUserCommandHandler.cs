using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Users;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.AddUser;

public class AddUserCommandHandler : IRequestHandler<AddUserRequest>
{
    private readonly IMapper _mapper;
    private readonly IValidator<AddUserRequest> _validator;
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IHashingService _hashingService;

    public AddUserCommandHandler(
        IMapper mapper,
        IValidator<AddUserRequest> validator,
        IEmailService emailService,
        IUserService userService,
        IRoleService roleService,
        IHashingService hashingService)
    {
        _mapper = mapper;
        _validator = validator;
        _emailService = emailService;
        _userService = userService;
        _roleService = roleService;
        _hashingService = hashingService;
    }
    
    public async Task Handle(AddUserRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<User>(request);

        model.HashPassword = _hashingService.HashPassword(request.Password);

        int roleId = request.RoleId ?? 1;

        var role = await _roleService.GetByIdAsync(roleId, cancellationToken);

        model.Roles = new List<Role> { role };

        await _userService.AddAsync(model, cancellationToken);
        
        var message = new MessageModel(
            model.Email,
            "Congratulations! You have been successfully registered.",
            $"Hello {model.UserName}! We are excited that you have started using our task tracking system.\n\n" +
            $"Hope, you will enjoy and achieve new goals, expanding your horizons.\n" +
            $"Thanks, The Task tracking system team.");

        await _emailService.SendEmailAsync(message);
    }
}