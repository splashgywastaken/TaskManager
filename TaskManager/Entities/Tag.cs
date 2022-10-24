using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Entities;

[Table("tag")]
public class Tag
{
    [Column("tag_id")]
    [Key]
    public int TagId { get; set; }

    [Column("tag_name")]
    public string TagName { get; set; }

    [Column("tag_description")]
    public string TagDescription { get; set; }

    // Many-to-many related properties
    public ICollection<Task> Tasks { get; set; }
    public List<TasksTags> TasksTags { get; set; }
}