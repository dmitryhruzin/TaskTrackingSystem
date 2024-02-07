using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Users;
using BuisnessLogicLayer.Responses.Users;
using MediatR;

namespace BuisnessLogicLayer.Commands.Users.GetUsers;

public class GetUsersCommandHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetUsersCommandHandler(
        IMapper mapper,
        IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var models = await _userService.GetAllAsync(cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetUserResponse>>(models));
    }
}