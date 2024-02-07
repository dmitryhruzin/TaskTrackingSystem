using AutoMapper;
using BuisnessLogicLayer.Responses.Assignments;

namespace BuisnessLogicLayer.Commands.Assignments.GetAssignment;

public class GetAssignmentResponseMappingProfile : Profile
{
    public GetAssignmentResponseMappingProfile()
    {
        CreateMap<Assignment, GetAssignmentResponse>()
            .ForMember(t => t.StatusName, t => t.MapFrom(n => n.Status.Name))
            .ForMember(t => t.ProjectName, t => t.MapFrom(n => n.Project.Name))
            .ForMember(t => t.ManagerUserName, t => t.MapFrom(n => n.Manager.UserName))
            .ForMember(t => t.UserProjectIds, t => t.MapFrom(m => m.UserProjects.Select(n => n.Id)));

    }
}