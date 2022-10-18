using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TaskManager.Entities;

[Table("task")]
public class Task
{
    [Column("task_id")]
    [Key]
    [JsonIgnore]
    public int TaskId { get; set; }

    [Column("task_name")]
    public string TaskName { get; set; }

    [Column("task_description")]
    [AllowNull]
    public string TaskDescription { get; set; }

    [Column("task_start_date")]
    public DateTime TaskStartDate { get; set; }

    [Column("task_finish_date")]
    public DateTime TaskFinishDate { get; set; }

    [Column("task_completion_status")]
    public bool TaskCompletionStatus { get; set; }

    // Many-to-one related properties
    [JsonIgnore]
    [Column("task_group_id")]
    public int? TaskGroupId { get; set; }
    [JsonIgnore]
    public TaskGroup? TaskGroup { get; set; }
}