using System.Windows.Input;

namespace TaskManagerWPF.Model;

public class Achievement
{
    public int AchievementId { get; set; }
    public string AchievementName { get; set; }
    public string AchievementDescription { get; set; }
    public int AchievementPoints { get; set; }
}