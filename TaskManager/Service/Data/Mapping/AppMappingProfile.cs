using AutoMapper;
using TaskManager.Models.Achievement;
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
        CreateMap<User, UserAchievementsModel>()
            .ForMember(
                dest => dest.UserAchievements, 
                opt => opt.MapFrom(src => src.Achievements)
                );
        CreateMap<User, UserAchievementsModel>()
            .ForMember(
                dest => dest.UserAchievements,
                opt => opt.MapFrom(src => src.Achievements)
            ).ReverseMap();

        // Achievement mapping
        CreateMap<AchievementModel, Achievement>();
        CreateMap<AchievementModel, Achievement>().ReverseMap();
    }
}