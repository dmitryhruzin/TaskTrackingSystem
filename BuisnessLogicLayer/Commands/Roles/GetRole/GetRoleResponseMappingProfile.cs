using AutoMapper;
using BuisnessLogicLayer.Responses.Roles;

namespace BuisnessLogicLayer.Commands.Roles.GetRole;

public class GetRoleResponseMappingProfile : Profile
{
    public GetRoleResponseMappingProfile()
    {
        CreateMap<Role, GetRoleResponse>();
    }
}