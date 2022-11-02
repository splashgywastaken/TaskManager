namespace TaskManager.Entities
{
    public partial class Project
    {
        public Project()
        {
            TaskGroups = new HashSet<TaskGroup>();
        }

        public int ProjectId { get; set; }
        public int? UserId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string? ProjectDescription { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectFinishDate { get; set; }
        public bool? ProjectCompletionStatus { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<TaskGroup> TaskGroups { get; set; }
    }
}
