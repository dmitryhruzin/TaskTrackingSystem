using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.ProjectStatuses;
using BuisnessLogicLayer.Responses.ProjectStatuses;
using MediatR;

namespace BuisnessLogicLayer.Commands.ProjectStatuses.GetProjectStatuses;

public class GetProjectStatusesCommandHandler : IRequestHandler<GetProjectStatusesRequest, GetProjectStatusesResponse>
{
    private readonly IMapper _mapper;
    private readonly IProjectStatusService _projectStatusService;

    public GetProjectStatusesCommandHandler(
        IMapper mapper,
        IProjectStatusService projectStatusService)
    {
        _mapper = mapper;
        _projectStatusService = projectStatusService;
    }
    
    public async Task<GetProjectStatusesResponse> Handle(GetProjectStatusesRequest request, CancellationToken cancellationToken)
    {
        var model = await _projectStatusService.GetAllAsync(cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetProjectStatusResponse>>(model));
    }
}