using AutoMapper;
using Microsoft.VisualBasic.CompilerServices;
using TaskManager.Models.Achievement;
using TaskManager.Models.Project;
using TaskManager.Models.Tag;
using TaskManager.Models.Task;
using TaskManager.Models.TaskGroup;
using TaskManager.Models.User;

namespace TaskManager.Service.Data.Mapping;

using TaskManager.Entities;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        // ProjectUser mapping
        CreateMap<User, UserLoginModel>().ReverseMap();
        CreateMap<User, UserRegistrationModel>().ReverseMap();
        CreateMap<User, UserDataModel>().ReverseMap();

        // ProjectUser projects mapping
        CreateMap<User, UserProjectsModel>().ForMember(
            dest => dest.UserProjects,
            opt => opt.MapFrom(src => src.Projects)
        ).ReverseMap();

        // ProjectUser achievements
        CreateMap<User, UserAchievementsModel>().ForMember(
            dest => dest.UserAchievements,
            opt => opt.MapFrom(src => src.UsersAchievementsAchievements)
        ).ReverseMap();

        // TaskGroupProject mapping
        CreateMap<Project, ProjectDataModel>().ReverseMap();
        CreateMap<Project, ProjectPostModel>().ReverseMap();
        CreateMap<Project, ProjectResponseModel>()
            .ForMember(
                dest => dest.ProjectTaskGroups, opt => opt.MapFrom(src => src.TaskGroups))
            .ReverseMap();

        // Tag mapping
        CreateMap<Tag, TagDataModel>().ReverseMap();

        // Task group mapping
        CreateMap<TaskGroup, TaskGroupDataModel>().ReverseMap();
        CreateMap<TaskGroup, TaskGroupProjectPostModel>().ReverseMap();
        CreateMap<TaskGroup, TaskGroupResponseModel>()
            .ForMember(
                dest => dest.TaskGroupTasks, 
                opt => opt.MapFrom(src => src.Tasks))
            .ReverseMap();

        // Task mapping
        CreateMap<Task, TaskModel>();
        CreateMap<Task, TaskModel>().ReverseMap();
        CreateMap<Task, TaskResponseModel>().ReverseMap();

        // Achievement mapping
        CreateMap<Achievement, AchievementModel>().ReverseMap();
    }
}