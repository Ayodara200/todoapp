using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoAppAyoNew;

namespace ToDoAppAyoNew
{
    public class TodoList
    {
        public List<User> Users { get; set; }

        public TodoList()
        {
            Users = new List<User>();
        }

        public void Register(string name, string email, string password)
        {
            Users.Add(new User(name, email, password));
        }

        public User Login(string email, string password)
        {
            return Users.FirstOrDefault(user => user.Email == email && user.Password == password);
        }

        public void AddTask(User user, string taskName, string description, DateTime dueDate, Priority priority)
        {
            user.Tasks.Add(new Task(taskName, description, dueDate, priority));
        }
        public List<Task> GetAllTasks(User user)
        {
            return user.Tasks;
        }
        public void EditTask(User user, EditTask editTask)
        {
            user.Tasks[editTask.TaskIndex].Name = editTask.TaskName;
            user.Tasks[editTask.TaskIndex].Description = editTask.Description;
            user.Tasks[editTask.TaskIndex].DueDate = editTask.DueDate;
            user.Tasks[editTask.TaskIndex].Priority = editTask.PriorityPriority;
        }

        public void DeleteTask(User user, int taskIndex)
        {
            user.Tasks.RemoveAt(taskIndex);
        }

        public void MarkTaskAsCompleted(User user, int taskIndex)
        {
            user.Tasks[taskIndex].Completed = true;
        }
    }

    internal class user
    {
        internal static readonly object Tasks;
    }
}

