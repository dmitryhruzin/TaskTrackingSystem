using AutoMapper;
using BuisnessLogicLayer.Requests.UserProjects;

namespace BuisnessLogicLayer.Commands.UserProjects.AddUserProject;

public class AddUserProjectMappingProfile : Profile
{
    public AddUserProjectMappingProfile()
    {
        CreateMap<AddUserProjectRequest, Position>();
    }
}