namespace TaskManager.Entities
{
    public partial class Task
    {
        public Task()
        {
            TasksTagsTags = new HashSet<Tag>();
        }

        public int TaskId { get; set; }
        public int? TaskTaskGroupId { get; set; }
        public string TaskName { get; set; } = null!;
        public string? TaskDescription { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskFinishDate { get; set; }
        public bool TaskCompletionStatus { get; set; }

        public virtual TaskGroup? TaskTaskGroup { get; set; }

        public virtual ICollection<Tag> TasksTagsTags { get; set; }
    }
}
