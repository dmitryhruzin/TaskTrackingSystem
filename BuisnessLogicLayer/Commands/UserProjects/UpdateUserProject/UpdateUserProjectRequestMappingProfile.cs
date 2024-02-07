using AutoMapper;
using BuisnessLogicLayer.Requests.UserProjects;

namespace BuisnessLogicLayer.Commands.UserProjects.UpdateUserProject;

public class UpdateUserProjectRequestMappingProfile : Profile
{
    public UpdateUserProjectRequestMappingProfile()
    {
        CreateMap<UpdateUserProjectRequest, Position>();
    }
}