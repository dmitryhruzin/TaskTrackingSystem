using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Users;
using BuisnessLogicLayer.Responses.Users;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.GetUsersByAssignmentId;

public class GetUsersByAssignmentIdCommandHandler : IRequestHandler<GetUsersByAssignmentIdRequest, GetUsersResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetUsersByAssignmentIdCommandHandler(
        IMapper mapper,
        IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<GetUsersResponse> Handle(GetUsersByAssignmentIdRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> expression = u =>
            u.UserProjects.Any(up => up.TaskId == request.Id);
        
        var models = await _userService.GetAllByExpressionAsync(expression, cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetUserResponse>>(models));
    }
}