using AutoMapper;
using BuisnessLogicLayer.Requests.Roles;

namespace BuisnessLogicLayer.Commands.Roles.UpdateRole;

public class UpdateRoleRequestMappingProfile : Profile
{
    public UpdateRoleRequestMappingProfile()
    {
        CreateMap<UpdateRoleRequest, Role>();
    }
}