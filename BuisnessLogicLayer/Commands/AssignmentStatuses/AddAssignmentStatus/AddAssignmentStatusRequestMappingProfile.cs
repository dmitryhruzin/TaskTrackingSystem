using AutoMapper;
using BuisnessLogicLayer.Requests.AssignmentStatuses;

namespace BuisnessLogicLayer.Commands.AssignmentStatuses.AddAssignmentStatus;

public class AddAssignmentStatusRequestMappingProfile : Profile
{
    public AddAssignmentStatusRequestMappingProfile()
    {
        CreateMap<AddAssignmentStatusRequest, AssignmentStatus>();
    }
}