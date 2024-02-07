using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.ProjectStatuses;
using BuisnessLogicLayer.Responses.ProjectStatuses;
using MediatR;

namespace BuisnessLogicLayer.Commands.ProjectStatuses.GetProjectStatus;

public class GetProjectStatusCommandHandler : IRequestHandler<GetProjectStatusRequest, GetProjectStatusResponse>
{
    private readonly IMapper _mapper;
    private readonly IProjectStatusService _projectStatusService;

    public GetProjectStatusCommandHandler(
        IMapper mapper,
        IProjectStatusService projectStatusService)
    {
        _mapper = mapper;
        _projectStatusService = projectStatusService;
    }
    
    public async Task<GetProjectStatusResponse> Handle(GetProjectStatusRequest request, CancellationToken cancellationToken)
    {
        var model = await _projectStatusService.GetByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<GetProjectStatusResponse>(model);
    }
}