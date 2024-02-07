using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Projects;
using BuisnessLogicLayer.Responses.Projects;
using MediatR;

namespace BuisnessLogicLayer.Commands.Projects.GetProjectsByUserId;

public class GetProjectsByUserIdCommandHandler : IRequestHandler<GetProjectsByUserIdRequest, GetProjectsResponse>
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public GetProjectsByUserIdCommandHandler(
        IMapper mapper,
        IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }
    
    public async Task<GetProjectsResponse> Handle(GetProjectsByUserIdRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<Project, bool>> expression = p =>
            p.Tasks.Any(t => t.UserProjects.Any(up => up.UserId == request.UserId));
        
        var models = await _projectService.GetAllByExpressionAsync(expression, cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetProjectResponse>>(models));
    }
}