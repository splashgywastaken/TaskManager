using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Entities;

[Table("tasks_tags")]
public class TasksTags
{
    [Column("tag_id")]
    public int TagId { get; set; }
    public Tag Tag { get; set; }

    [Column("task_id")]
    public int TaskId { get; set; }
    public Task Task { get; set; }
}