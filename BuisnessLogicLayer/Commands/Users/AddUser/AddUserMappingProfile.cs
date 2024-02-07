using AutoMapper;
using BuisnessLogicLayer.Requests.Users;

namespace BuisnessLogicLayer.Commands.Users.AddUser;

public class AddUserMappingProfile : Profile
{
    public AddUserMappingProfile()
    {
        CreateMap<AddUserRequest, User>()
            .ForMember(u => u.HashPassword, o => o.Ignore());
    }
}