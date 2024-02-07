using AutoMapper;
using BuisnessLogicLayer.Responses.AssignmentStatuses;

namespace BuisnessLogicLayer.Commands.AssignmentStatuses.GetAssignmentStatus;

public class GetAssignmentStatusResponseMappingProfile : Profile
{
    public GetAssignmentStatusResponseMappingProfile()
    {
        CreateMap<AssignmentStatus, GetAssignmentStatusResponse>();
    }
}