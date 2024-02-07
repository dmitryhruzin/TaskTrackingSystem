using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Users;
using BuisnessLogicLayer.Responses.Users;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.GetUsersByProjectId;

public class GetUsersByProjectIdCommandHandler : IRequestHandler<GetUsersByProjectIdRequest, GetUsersResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetUsersByProjectIdCommandHandler(
        IMapper mapper,
        IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<GetUsersResponse> Handle(GetUsersByProjectIdRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> expression = u =>
            u.UserProjects.Any(up => up.Task.ProjectId == request.ProjectId);
        
        var models = await _userService.GetAllByExpressionAsync(expression, cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetUserResponse>>(models));
    }
}