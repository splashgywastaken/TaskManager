using Task = TaskManager.Entities.Task;

namespace TaskManager.Entities
{
    public partial class Tag
    {
        public Tag()
        {
            Tasks = new HashSet<Task>();
        }

        public int TagId { get; set; }
        public string TagName { get; set; } = null!;
        public string? TagDescription { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
