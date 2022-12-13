using System.Windows.Input;

namespace TaskManagerWPF.Model;

public class Achievement
{
    public int AchievementId { get; set; }
    public string AchievementName { get; set; } = null!;
    public string AchievementDescription { get; set; } = null!;
    public int AchievementPoints { get; set; }
}