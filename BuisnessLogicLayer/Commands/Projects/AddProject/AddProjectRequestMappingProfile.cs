using AutoMapper;
using BuisnessLogicLayer.Requests.Projects;

namespace BuisnessLogicLayer.Commands.Projects.AddProject;

public class AddProjectRequestMappingProfile : Profile
{
    public AddProjectRequestMappingProfile()
    {
        CreateMap<AddProjectRequest, Project>();
    }
}