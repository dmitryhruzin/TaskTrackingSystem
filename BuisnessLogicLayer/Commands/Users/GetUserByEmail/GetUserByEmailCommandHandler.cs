using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Users;
using BuisnessLogicLayer.Responses.Users;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.GetUserByEmail;

public class GetUserByEmailCommandHandler : IRequestHandler<GetUserByEmailRequest, GetUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetUserByEmailCommandHandler(
        IMapper mapper,
        IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<GetUserResponse> Handle(GetUserByEmailRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> expression = u => u.Email == request.Email;
        
        var model = (await _userService.GetAllByExpressionAsync(expression, cancellationToken)).First();

        return _mapper.Map<GetUserResponse>(model);
    }
}