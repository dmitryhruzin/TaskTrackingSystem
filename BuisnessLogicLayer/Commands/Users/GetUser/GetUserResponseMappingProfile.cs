using AutoMapper;
using BuisnessLogicLayer.Responses.Users;

namespace BuisnessLogicLayer.Commands.Users.GetUser;

public class GetUserResponseMappingProfile : Profile
{
    public GetUserResponseMappingProfile()
    {
        CreateMap<User, GetUserResponse>()
            .ForMember(t => t.TaskIds, t => t.MapFrom(m => m.Tasks.Select(n => n.Id)))
            .ForMember(t => t.UserProjectIds, t => t.MapFrom(m => m.UserProjects.Select(n => n.Id)));
    }
}