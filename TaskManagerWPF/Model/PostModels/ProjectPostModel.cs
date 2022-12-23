using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using TaskManagerWPF.Model.Project;

namespace TaskManagerWPF.Model.PostModels;

public class CompositeProjectTaskGroupModel
{
    [JsonProperty("projectPostModel")] 
    public ProjectPostModel ProjectPostModel { get; set; } = null!;

    [JsonProperty("taskGroupProjectPostModel")]
    public TaskGroupPostModel TaskGroupPostModel { get; set; } = null!;
}

public class ProjectPostModel
{
    [JsonProperty("project_user_id")]
    public int ProjectUserId { get; set; }

    [JsonProperty("project_id")]
    public int ProjectId { get; set; }

    [JsonProperty("project_name")]
    public string ProjectName { get; set; } = null!;

    [JsonProperty("project_description")]
    public string ProjectDescription { get; set; } = null!;

    [JsonProperty("project_start_date")]
    public DateTime ProjectStartDate { get; set; } = DateTime.UtcNow;

    [JsonProperty("project_finish_date")]
    public DateTime ProjectFinishDate { get; set; } = DateTime.UtcNow.AddDays(30);

    [JsonProperty("project_completion_status")]
    public bool ProjectCompletionStatus { get; set; } = false;

    [JsonProperty("project_task_groups")]
    public List<TaskGroupWithAllData> ProjectTaskGroups { get; set; } = null!;
}
public class TaskGroupPostModel
{
    [JsonProperty("task_group_name")] 
    public string TaskGroupName { get; set; } = null!;

    [JsonProperty("task_group_description")]
    public string TaskDescription { get; set; } = null!;
}