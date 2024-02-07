using AutoMapper;
using BuisnessLogicLayer.Requests.Users;

namespace BuisnessLogicLayer.Commands.Users.UpdateUser;

public class UpdateUserRequestMappingProfile : Profile
{
    public UpdateUserRequestMappingProfile()
    {
        CreateMap<UpdateUserRequest, User>();
    }
}