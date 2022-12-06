using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerWPF.Model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string TagDescription { get; set; }
        public Tag(string tagName, string tagDescription)
        {
            TagName = tagName;
            TagDescription = tagDescription;
        }
    }
}
