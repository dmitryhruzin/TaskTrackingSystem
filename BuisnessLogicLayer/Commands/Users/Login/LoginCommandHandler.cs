using System.Linq.Expressions;
using System.Security.Claims;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Users;
using BuisnessLogicLayer.Responses.Tokens;
using BuisnessLogicLayer.Validation;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.Login;

public class LoginCommandHandler : IRequestHandler<LoginRequest, GetTokenResponse>
{
    private readonly IGenerateTokenService _generateTokenService;
    private readonly IUserService _userService;
    private readonly IValidator<LoginRequest> _validator;
    private readonly IHashingService _hashingService;
    private readonly IRoleService _roleService;

    public LoginCommandHandler(
        IGenerateTokenService generateTokenService,
        IUserService userService,
        IValidator<LoginRequest> validator,
        IHashingService hashingService,
        IRoleService roleService)
    {
        _generateTokenService = generateTokenService;
        _userService = userService;
        _validator = validator;
        _hashingService = hashingService;
        _roleService = roleService;
    }
    
    public async Task<GetTokenResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        Expression<Func<User, bool>> expression = u => u.Email == request.Login;
        
        var user = (await _userService.GetAllByExpressionAsync(expression, cancellationToken)).First();

        if (user is null || !_hashingService.VerifyPassword(request.Password, user.HashPassword))
            throw new TaskTrackingException("User not found");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };
        
        Expression<Func<Role, bool>> roleExpression = r => r.Users.Any(u => u.Id == user.Id);
        
        var roles = await _roleService.GetAllByExpressionAsync(roleExpression, cancellationToken);

        foreach (var item in roles)
            claims.Add(new Claim(ClaimTypes.Role, item.Name));

        var accessToken = _generateTokenService.GenerateAccessToken(claims);

        var refreshToken = _generateTokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;

        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

        await _userService.UpdateAsync(user, cancellationToken);

        return new GetTokenResponse(accessToken, refreshToken);
    }
}