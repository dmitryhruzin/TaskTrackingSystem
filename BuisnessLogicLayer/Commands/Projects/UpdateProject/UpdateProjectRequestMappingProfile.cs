using AutoMapper;
using BuisnessLogicLayer.Requests.Projects;

namespace BuisnessLogicLayer.Commands.Projects.UpdateProject;

public class UpdateProjectRequestMappingProfile : Profile
{
    public UpdateProjectRequestMappingProfile()
    {
        CreateMap<UpdateProjectRequest, Project>();
    }
}