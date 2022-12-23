using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaskManagerWPF.Model.Project;

public class TaskGroupWithAllData
{
    public TaskGroupWithAllData()
    {
    }
    public TaskGroupWithAllData(int taskGroupId, string taskGroupName, string taskGroupDescription)
    {
        TaskGroupId = taskGroupId;
        TaskGroupName = taskGroupName;
        TaskGroupDescription = taskGroupDescription;
    }

    [JsonProperty("task_group_id")]
    public int TaskGroupId { get; set; }

    [JsonProperty("task_group_name")]
    public string TaskGroupName { get; set; } = null!;

    [JsonProperty("task_group_description")]
    public string TaskGroupDescription { get; set; } = null!;

    [JsonProperty("task_group_tasks")]
    public List<TaskWithAllData> TaskGroupTasks { get; set; } = null!;
}