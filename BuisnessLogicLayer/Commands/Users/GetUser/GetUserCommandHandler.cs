using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Users;
using BuisnessLogicLayer.Responses.Users;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.GetUser;

public class GetUserCommandHandler : IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetUserCommandHandler(
        IMapper mapper,
        IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var model = await _userService.GetByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<GetUserResponse>(model);
    }
}