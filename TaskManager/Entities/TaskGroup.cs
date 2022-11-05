namespace TaskManager.Entities
{
    public partial class TaskGroup
    {
        public TaskGroup()
        {
            Tasks = new HashSet<Task>();
        }

        public int TaskGroupId { get; set; }
        public int? TaskGroupProjectId { get; set; }
        public string TaskGroupName { get; set; } = null!;
        public string? TaskGroupDescription { get; set; }

        public virtual Project? TaskGroupProject { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
