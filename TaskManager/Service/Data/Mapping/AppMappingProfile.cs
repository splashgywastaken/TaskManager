using AutoMapper;
using Microsoft.VisualBasic.CompilerServices;
using TaskManager.Models.Achievement;
using TaskManager.Models.Project;
using TaskManager.Models.Tag;
using TaskManager.Models.User;

namespace TaskManager.Service.Data.Mapping;

using TaskManager.Entities;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        // ProjectUser mapping
        CreateMap<User, UserDataModel>();
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

        CreateMap<Tag, TagDataModel>().ReverseMap();

        // Achievement mapping
        CreateMap<Achievement, AchievementModel>().ReverseMap();
    }
}