using AutoMapper;
using BuisnessLogicLayer.Responses.ProjectStatuses;

namespace BuisnessLogicLayer.Commands.ProjectStatuses.GetProjectStatus;

public class GetProjectStatusResponseMappingProfile : Profile
{
    public GetProjectStatusResponseMappingProfile()
    {
        CreateMap<ProjectStatus, GetProjectStatusResponse>();
    }
}