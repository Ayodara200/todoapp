using System;
using System.Collections.Generic;
using System.Text;
using ToDoAppAyoNew;

namespace ToDoAppAyoNew
{
    public class EditTask
    {
        public int TaskIndex { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority PriorityPriority { get; set; }
    }
}
