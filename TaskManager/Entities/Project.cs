using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TaskManager.Entities;

[Table("project")]
public class Project
{
    [Column("project_id")]
    [Key]
    [JsonIgnore]
    public int ProjectId { get; set; }

    [Column("project_name")]
    public string ProjectName { get; set; }

    [Column("project_description")]
    [AllowNull]
    public string ProjectDescription { get; set; }

    [Column("project_start_date")]
    public DateTime ProjectStartDate { get; set; }

    [Column("project_finish_date")]
    public DateTime ProjectEndDate { get; set; }

    // Many-to-one property
    [JsonIgnore]
    [Column("user_id")]
    public int? UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }

    // One-to-many 
    [JsonIgnore]
    public List<TaskGroup>? TaskGroups { get; set; }
}