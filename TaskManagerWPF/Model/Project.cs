using System;
using System.Collections.Generic;

namespace TaskManagerWPF.Model;

public class Project
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ProjectDescription { get; set; } = null!;
    public DateTime ProjectStartDate { get; set; }
    public DateTime ProjectFinishDate { get; set; }
    public bool ProjectCompletionStatus { get; set; }
    public List<TaskGroup> ProjectTaskGroups { get; set; } = null!;
}