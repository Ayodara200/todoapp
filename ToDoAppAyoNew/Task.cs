using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppAyoNew
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }

        public Task(string name, string description, DateTime dueDate, Priority priority)
        {
            Name = name;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            Completed = false;
        }
    }
    public enum Priority
    {
        Low,
        Medium,
        High
    }
}
