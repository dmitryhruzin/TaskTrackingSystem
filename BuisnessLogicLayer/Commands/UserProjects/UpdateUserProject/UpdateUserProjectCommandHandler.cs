using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.UserProjects;
using MediatR;

namespace BuisnessLogicLayer.Commands.UserProjects.UpdateUserProject;

public class UpdateUserProjectCommandHandler : IRequestHandler<UpdateUserProjectRequest>
{
    private readonly IUserProjectService _userProjectService;
    private readonly IMapper _mapper;

    public UpdateUserProjectCommandHandler(
        IUserProjectService userProjectService,
        IMapper mapper)
    {
        _userProjectService = userProjectService;
        _mapper = mapper;
    }
    
    public async Task Handle(UpdateUserProjectRequest request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<UserProject>(request);

        await _userProjectService.UpdateAsync(model, cancellationToken);
    }
}