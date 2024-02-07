using AutoMapper;
using BuisnessLogicLayer.Requests.Assignments;

namespace BuisnessLogicLayer.Commands.Assignments.UpdateAssignment;

public class UpdateAssignmentRequestMappingProfile : Profile
{
    public UpdateAssignmentRequestMappingProfile()
    {
        CreateMap<UpdateAssignmentRequest, Assignment>();
    }
}