using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerWPF.Model
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = null!;
        public string TaskDescription { get; set; } = null!;
        public bool TaskCompletionStatus { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskFinisDate { get; set; }
        public List<Tag> TaskTags { get; set; }
    }
}
