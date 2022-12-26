using System;
using System.Collections.Generic;
using TaskManagerWPF.Model;

namespace TaskManagerWPF.ViewModel
{
    internal class TaskViewModel
    {
        public string TaskName { get; set; } = "Sample task name";
        public string TaskDescription { get; set; } =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ";
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskFinishDate { get; set; }
        public bool TaskCompletionStatus { get; set; }
        public List<Tag> TaskTags { get; set; } = new()
        {
            new Tag("Sample tag name1", "Sample tag description"),
            new Tag("Sample tag name2", "Sample tag description"),
            new Tag("Sample tag name3", "Sample tag description"),
            new Tag("Sample tag name4", "Sample tag description")
        };
    }
}
