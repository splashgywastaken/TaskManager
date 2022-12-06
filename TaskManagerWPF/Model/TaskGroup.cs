using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerWPF.Model
{
    public class TaskGroup
    {
        public int TaskGroupId { get; set; }
        public string TaskGroupName { get; set; } = null!;
        public string TaskGroupDescription { get; set; } = null!;
        public List<Task> TaskGroupTasks { get; set; } = null!;
    }
}
