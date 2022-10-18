using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TaskManager.Entities;


[Table("task_group")]
public class TaskGroup
{
    [Column("task_group_id")]
    [Key]
    [JsonIgnore]
    public int TaskGroupId { get; set; }

    [Column("task_group_name")]
    public string TaskGroupName { get; set; }

    [Column("task_group_description")]
    [AllowNull]
    public string TaskGroupDescription { get; set; }

    // Many-to-one related property
    [JsonIgnore]
    [Column("project_id")]
    public int? ProjectId { get; set; }

    [JsonIgnore]
    public Project? Project { get; set; }

    // One-to-many related property
    [JsonIgnore]
    public List<Task>? Tasks { get; set; }
}
