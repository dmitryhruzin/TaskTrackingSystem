using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Users;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordRequest>
{
    private readonly IUserService _userService;
    private readonly IHashingService _hashingService;
    private readonly IValidator<ResetPasswordRequest> _validator;

    public ResetPasswordCommandHandler(
        IUserService userService,
        IHashingService hashingService,
        IValidator<ResetPasswordRequest> validator)
    {
        _userService = userService;
        _hashingService = hashingService;
        _validator = validator;
    }
    
    public async Task Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        Expression<Func<User, bool>> expression = u => u.Email == request.Email;
        
        var user = (await _userService.GetAllByExpressionAsync(expression, cancellationToken)).First();

        if (user.RefreshToken == request.Token)
        {
            user.HashPassword = _hashingService.HashPassword(request.Password);

            await _userService.UpdateAsync(user, cancellationToken);
        }
    }
}