namespace TaskManager.Entities
{
    public partial class Task
    {
        public Task()
        {
            Tags = new HashSet<Tag>();
        }

        public int TaskId { get; set; }
        public int? TaskGroupId { get; set; }
        public string TaskName { get; set; } = null!;
        public string? TaskDescription { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskFinishDate { get; set; }
        public bool TaskCompletionStatus { get; set; }

        public virtual TaskGroup? TaskGroup { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
