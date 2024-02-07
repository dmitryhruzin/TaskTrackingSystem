using AutoMapper;
using BuisnessLogicLayer.Requests.Roles;

namespace BuisnessLogicLayer.Commands.Roles.AddRole;

public class AddRoleRequestMappingProfile : Profile
{
    public AddRoleRequestMappingProfile()
    {
        CreateMap<AddRoleRequest, Role>();
    }
}