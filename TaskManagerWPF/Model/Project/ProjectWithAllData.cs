﻿using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace TaskManagerWPF.Model.Project;

public class ProjectWithAllData
{
    public ProjectWithAllData()
    {
    }
    public ProjectWithAllData(int projectId, string projectName, string projectDescription)
    {
        ProjectId = projectId;
        ProjectName = projectName;
        ProjectDescription = projectDescription;
    }

    [JsonProperty("project_user_id")] 
    public int ProjectUserId { get; set; }

    [JsonProperty("project_id")]
    public int ProjectId { get; set; }
    
    [JsonProperty("project_name")]
    public string ProjectName { get; set; } = null!;

    [JsonProperty("project_description")] 
    public string ProjectDescription { get; set; } = null!;
    
    [JsonProperty("project_start_date")]
    public DateTime ProjectStartDate { get; set; }

    [JsonProperty("project_finish_date")]
    public DateTime ProjectFinishDate { get; set; }

    [JsonProperty("project_completion_status")]
    public bool? ProjectCompletionStatus { get; set; }
    
    [JsonProperty("project_task_groups")]
    public List<TaskGroupWithAllData> ProjectTaskGroups { get; set; } = null!;
}