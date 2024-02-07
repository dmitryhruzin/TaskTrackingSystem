using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.UserProjects;
using BuisnessLogicLayer.Responses.Positions;
using BuisnessLogicLayer.Responses.UserProjects;
using MediatR;

namespace BuisnessLogicLayer.Commands.UserProjects.GetUserProject;

public class GetUserProjectCommandHandler : IRequestHandler<GetUserProjectRequest, GetUserProjectResponse>
{
    private readonly IUserProjectService _userProjectService;
    private readonly IMapper _mapper;

    public GetUserProjectCommandHandler(
        IUserProjectService userProjectService,
        IMapper mapper)
    {
        _userProjectService = userProjectService;
        _mapper = mapper;
    }
    
    public async Task<GetUserProjectResponse> Handle(GetUserProjectRequest request, CancellationToken cancellationToken)
    {
        var model = await _userProjectService.GetByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<GetUserProjectResponse>(model);
    }
}