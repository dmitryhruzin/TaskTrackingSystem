using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.UserProjects;
using BuisnessLogicLayer.Responses.UserProjects;
using MediatR;

namespace BuisnessLogicLayer.Commands.UserProjects.GetUserProjects;

public class GetUserProjectsCommandHandler : IRequestHandler<GetUserProjectsRequest, GetUserProjectsResponse>
{
    private readonly IUserProjectService _userProjectService;
    private readonly IMapper _mapper;

    public GetUserProjectsCommandHandler(
        IUserProjectService userProjectService,
        IMapper mapper)
    {
        _userProjectService = userProjectService;
        _mapper = mapper;
    }
    
    public async Task<GetUserProjectsResponse> Handle(GetUserProjectsRequest request, CancellationToken cancellationToken)
    {
        var models = await _userProjectService.GetAllAsync(cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetUserProjectResponse>>(models));
    }
}