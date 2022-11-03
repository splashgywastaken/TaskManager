using AutoMapper;
using Microsoft.VisualBasic.CompilerServices;
using TaskManager.Models.Achievement;
using TaskManager.Models.Project;
using TaskManager.Models.User;

namespace TaskManager.Service.Data.Mapping;

using TaskManager.Entities;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        // User mapping
        CreateMap<User, UserDataModel>();
        CreateMap<User, UserDataModel>().ReverseMap();

        // User projects mapping
        CreateMap<User, UserProjectsModel>().ForMember(
            dest => dest.UserProjects,
            opt => opt.MapFrom(src => src.Projects)
        ).ReverseMap();

        // User achievements
        CreateMap<User, UserAchievementsModel>().ForMember(
            dest => dest.UserAchievements,
            opt => opt.MapFrom(src => src.Achievements)
        ).ReverseMap();

        // Project mapping
        CreateMap<Project, ProjectDataModel>();
        CreateMap<Project, ProjectDataModel>().ReverseMap();

        // Achievement mapping
        CreateMap<Achievement, AchievementModel>().ReverseMap();
    }
}