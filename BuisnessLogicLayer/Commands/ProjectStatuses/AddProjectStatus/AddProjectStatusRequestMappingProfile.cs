using AutoMapper;
using BuisnessLogicLayer.Requests.ProjectStatuses;

namespace BuisnessLogicLayer.Commands.ProjectStatuses.AddProjectStatus;

public class AddProjectStatusRequestMappingProfile : Profile
{
    public AddProjectStatusRequestMappingProfile()
    {
        CreateMap<AddProjectStatusRequest, ProjectStatus>();
    }
}