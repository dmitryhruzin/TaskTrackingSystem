using AutoMapper;
using BuisnessLogicLayer.Requests.AssignmentStatuses;

namespace BuisnessLogicLayer.Commands.AssignmentStatuses.UpdateAssignmentStatus;

public class UpdateAssignmentStatusRequestMappingProfile : Profile
{
    public UpdateAssignmentStatusRequestMappingProfile()
    {
        CreateMap<UpdateAssignmentStatusRequest, AssignmentStatus>();
    }
}