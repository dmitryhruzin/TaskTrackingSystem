using AutoMapper;
using BuisnessLogicLayer.Requests.Assignments;

namespace BuisnessLogicLayer.Commands.Assignments.AddAssignment;

public class AddAssignmentRequestMappingProfile : Profile
{
    public AddAssignmentRequestMappingProfile()
    {
        CreateMap<AddAssignmentRequest, Assignment>();
    }
}