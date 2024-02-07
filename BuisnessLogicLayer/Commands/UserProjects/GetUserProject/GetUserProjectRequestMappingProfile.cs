using AutoMapper;
using BuisnessLogicLayer.Responses.UserProjects;

namespace BuisnessLogicLayer.Commands.UserProjects.GetUserProject;

public class GetUserProjectRequestMappingProfile : Profile
{
    public GetUserProjectRequestMappingProfile()
    {
        CreateMap<UserProject, GetUserProjectResponse>()
            .ForMember(t => t.UserName, t => t.MapFrom(n => n.User.UserName))
            .ForMember(t => t.PositionName, t => t.MapFrom(n => n.Position.Name))
            .ForMember(t => t.TaskName, t => t.MapFrom(n => n.Task.Name))
            .ForMember(t => t.UserEmail, t => t.MapFrom(n => n.User.Email));
    }
}