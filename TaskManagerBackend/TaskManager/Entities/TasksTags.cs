namespace TaskManager.Entities
{
    public class TasksTags
    {
        public int TasksTagsTaskId { get; set; }
        public Task Task { get; set; } = null!;
        public int TasksTagsTagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
