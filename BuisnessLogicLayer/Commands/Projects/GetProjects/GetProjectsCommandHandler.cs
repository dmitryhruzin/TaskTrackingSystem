using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Projects;
using BuisnessLogicLayer.Responses.Projects;
using MediatR;

namespace BuisnessLogicLayer.Commands.Projects.GetProjects;

public class GetProjectsCommandHandler : IRequestHandler<GetProjectsRequest, GetProjectsResponse>
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public GetProjectsCommandHandler(
        IMapper mapper,
        IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }
    
    public async Task<GetProjectsResponse> Handle(GetProjectsRequest request, CancellationToken cancellationToken)
    {
        var models = await _projectService.GetAllAsync(cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetProjectResponse>>(models));
    }
}