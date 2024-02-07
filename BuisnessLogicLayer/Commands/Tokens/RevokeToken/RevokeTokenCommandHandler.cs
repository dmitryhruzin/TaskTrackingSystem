using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Tokens;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Tokens.RevokeToken;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenRequest>
{
    private readonly IGenerateTokenService _generateTokenService;
    private readonly IUserService _userService;
    private readonly IValidator<RevokeTokenRequest> _validator;

    public RevokeTokenCommandHandler(
        IGenerateTokenService generateTokenService,
        IUserService userService,
        IValidator<RevokeTokenRequest> validator)
    {
        _generateTokenService = generateTokenService;
        _userService = userService;
        _validator = validator;
    }
    
    public async Task Handle(RevokeTokenRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var principal = _generateTokenService.GetPrincipalFromExpiredToken(request.AccessToken);

        var userName = principal.Identity.Name;

        Expression<Func<User, bool>> expression = u => u.UserName == userName;

        var user = (await _userService.GetAllByExpressionAsync(expression, cancellationToken)).First();

        user.RefreshToken = null;

        await _userService.UpdateAsync(user, cancellationToken);
    }
}