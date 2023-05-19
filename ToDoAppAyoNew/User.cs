using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoAppAyoNew
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            Tasks = new List<Task>();
        }
    }
}