﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TaskManagerWPF.Model.Project;

public class TaskWithAllData
{
    public TaskWithAllData(string taskName, string taskDescription)
    {
        TaskName = taskName;
        TaskDescription = taskDescription;
    }

    public TaskWithAllData()
    {
    }

    [JsonProperty("task_id")]
    public int TaskId { get; set; }

    [JsonProperty("task_task_group_id")]
    public int TaskTaskGroupId { get; set; }

    [JsonProperty("task_name")]
    public string TaskName { get; set; } = null!;

    [JsonProperty("task_description")]
    public string TaskDescription { get; set; } = null!;

    [JsonProperty("task_completion_status")]
    public bool TaskCompletionStatus { get; set; }

    [JsonProperty("task_start_date")]
    public DateTime TaskStartDate { get; set; }

    [JsonProperty("task_finish_date")]
    public DateTime TaskFinishDate { get; set; }
}