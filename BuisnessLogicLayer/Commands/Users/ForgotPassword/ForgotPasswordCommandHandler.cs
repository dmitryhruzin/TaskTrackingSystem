using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Requests.Users;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;

namespace BuisnessLogicLayer.Commands.Users.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordRequest>
{
    private readonly IUserService _userService;
    private readonly IGenerateTokenService _generateTokenService;
    private readonly IValidator<ForgotPasswordRequest> _validator;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(
        IUserService userService,
        IGenerateTokenService generateTokenService,
        IValidator<ForgotPasswordRequest> validator,
        IEmailService emailService)
    {
        _userService = userService;
        _generateTokenService = generateTokenService;
        _validator = validator;
        _emailService = emailService;
    }

    public async Task Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        Expression<Func<User, bool>> expression = u => u.Email == request.Email;

        var user = (await _userService.GetAllByExpressionAsync(expression, cancellationToken)).First();

        if (user is not null)
        {
            var token = _generateTokenService.GenerateRefreshToken();
        
            user.RefreshToken = token;

            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userService.UpdateAsync(user, cancellationToken);

            var parameters = new Dictionary<string, string>
            {
                { "token", token },
                { "email", user.Email }
            };

            var callback = QueryHelpers.AddQueryString(request.ClientUri, parameters);

            var message = new MessageModel(
                user.Email,
                "Password reset code.",
                $"Click this link to reset your password:\n{callback}\n\n" +
                $"If it wasn't you, ignore this email please.\n" +
                $"Thanks, The Task tracking system team.");

            await _emailService.SendEmailAsync(message);
        }
    }
}