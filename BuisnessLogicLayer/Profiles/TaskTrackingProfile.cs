using AutoMapper;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace BuisnessLogicLayer.Profiles
{
    public class TaskTrackingProfile : Profile
    {
        public TaskTrackingProfile()
        {
            CreateMap<ProjectStatus, StatusModel>().ReverseMap();

            CreateMap<AssignmentStatus, StatusModel>().ReverseMap();

            CreateMap<Position, PositionModel>().ReverseMap();

            CreateMap<User, UserModel>()
                .ForMember(t => t.TaskIds, t => t.MapFrom(m => m.Tasks.Select(n => n.Id)))
                .ForMember(t => t.UserProjectIds, t => t.MapFrom(m => m.UserProjects.Select(n => n.Id)));

            CreateMap<UserModel, User>();

            CreateMap<User, RegisterUserModel>().ReverseMap();

            CreateMap<IdentityRole<int>, RoleModel>().ReverseMap();

            CreateMap<Project, ProjectModel>()
                .ForMember(t => t.StatusName, t => t.MapFrom(n => n.Status.Name))
                .ForMember(t => t.TaskIds, t => t.MapFrom(m => m.Tasks.Select(n => n.Id)));

            CreateMap<ProjectModel, Project>();

            CreateMap<Assignment, TaskModel>()
                .ForMember(t => t.StatusName, t => t.MapFrom(n => n.Status.Name))
                .ForMember(t => t.ProjectName, t => t.MapFrom(n => n.Project.Name))
                .ForMember(t => t.ManagerUserName, t => t.MapFrom(n => n.Manager.UserName))
                .ForMember(t => t.UserProjectIds, t => t.MapFrom(m => m.UserProjects.Select(n => n.Id)));

            CreateMap<TaskModel, Assignment>();

            CreateMap<UserProject, UserProjectModel>()
                .ForMember(t => t.UserName, t => t.MapFrom(n => n.User.UserName))
                .ForMember(t => t.PositionName, t => t.MapFrom(n => n.Position.Name))
                .ForMember(t => t.TaskName, t => t.MapFrom(n => n.Task.Name))
                .ForMember(t => t.UserEmail, t => t.MapFrom(n => n.User.Email));

            CreateMap<UserProjectModel, UserProject>();
        }
    }
}
