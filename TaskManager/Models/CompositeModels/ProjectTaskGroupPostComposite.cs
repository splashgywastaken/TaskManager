using TaskManager.Models.Project;
using TaskManager.Models.TaskGroup;

namespace TaskManager.Models.CompositeModels
{
    public class ProjectTaskGroupPostComposite
    {
        public ProjectPostModel ProjectPostModel { get; set; } = null!;
        public TaskGroupProjectPostModel TaskGroupProjectPostModel { get; set; } = null!;
    }
}
