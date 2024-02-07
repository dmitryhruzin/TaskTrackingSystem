using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Tokens;
using BuisnessLogicLayer.Responses.Tokens;
using BuisnessLogicLayer.Validation;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Tokens.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenRequest, GetTokenResponse>
{
    private readonly IGenerateTokenService _generateTokenService;
    private readonly IUserService _userService;
    private readonly IValidator<RefreshTokenRequest> _validator;
    
    public RefreshTokenCommandHandler(
        IGenerateTokenService generateTokenService,
        IUserService userService,
        IValidator<RefreshTokenRequest> validator)
    {
        _generateTokenService = generateTokenService;
        _userService = userService;
        _validator = validator;
    }
    
    public async Task<GetTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var principal = _generateTokenService.GetPrincipalFromExpiredToken(request.AccessToken);

        var userName = principal.Identity.Name;

        Expression<Func<User, bool>> expression = u => u.UserName == userName;

        var user = (await _userService.GetAllByExpressionAsync(expression, cancellationToken)).First();

        if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new TaskTrackingException("Invalid request");

        var newAccessToken = _generateTokenService.GenerateAccessToken(principal.Claims);

        return new GetTokenResponse(newAccessToken, request.RefreshToken);
    }
}