using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToDoAppAyoNew
{
    public class Program
    {
        static TodoList todoList = new TodoList();
        static User currentUser;
        //public static Priority priority;

        static void Main(string[] args)
        {
            List<Task> Tasks = new List<Task>();
            string nameRegex = @"^[a-zA-Z]+$";
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome to Todo Console Application!");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Please enter your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        string name = "";
                        while (string.IsNullOrWhiteSpace(name))
                        {
                            Console.Write("Please enter your name: ");
                            name = Console.ReadLine();
                            if (!Regex.IsMatch(name, nameRegex))
                            {
                                Console.WriteLine("Invalid name format. Please try again.");
                                name = "";
                            }
                        }

                        string email = "";
                        while (string.IsNullOrWhiteSpace(email))
                        {
                            Console.Write("Please enter your email address: ");
                            email = Console.ReadLine();
                            if (!Regex.IsMatch(email, emailRegex))
                            {
                                Console.WriteLine("Invalid email format. Please try again.");
                                email = "";
                            }
                        }

                        string password = "";
                        string passwordConfirm = "";
                        bool passwordsMatch = false;

                        while (string.IsNullOrWhiteSpace(password) || !passwordsMatch)
                        {
                            Console.Write("Please enter your password: ");
                            password = Console.ReadLine();

                            Console.Write("Please confirm your password: ");
                            passwordConfirm = Console.ReadLine();

                            if (!Regex.IsMatch(password, passwordRegex))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid password format. Please try again.");
                                password = "";
                                passwordConfirm = "";
                                Console.ResetColor();
                            }
                            else if (password != passwordConfirm)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Passwords do not match. Please try again.");
                                password = "";
                                passwordConfirm = "";
                                Console.ResetColor();
                            }
                            else
                            {
                                passwordsMatch = true;
                            }
                        }

                        todoList.Register(name, email, password);
                        Console.WriteLine("Registration successful!");
                        Console.WriteLine();
                        currentUser = todoList.Login(email, password);
                        if (currentUser != null)
                        {
                            Console.WriteLine("Login successful!");
                            Console.WriteLine();
                            ShowTaskMenu(Tasks);
                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password!");
                            Console.WriteLine();
                        }
                        break;

                    case "2":
                        Console.WriteLine("Please enter your email: ");
                        string loginEmail = Console.ReadLine();
                        while (!Regex.IsMatch(loginEmail, emailRegex))
                        {
                            Console.WriteLine("Invalid email format!");
                            Console.Write("Please enter your email address: ");
                            loginEmail = Console.ReadLine();

                        }
                        Console.WriteLine("Please enter your password: ");
                        string loginPassword = Console.ReadLine();
                        while (!Regex.IsMatch(loginPassword, passwordRegex))
                        {
                            Console.WriteLine("Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
                            Console.WriteLine("Please enter your password: ");
                            loginPassword = Console.ReadLine();
                        }

                        currentUser = todoList.Login(loginEmail, loginPassword);
                        if (currentUser != null)
                        {
                            Console.WriteLine("Login successful!");
                            Console.WriteLine();
                            ShowTaskMenu(Tasks);
                        }
                        else
                        {
                            Console.WriteLine("Invalid email or password!");
                            Console.WriteLine();
                        }
                        break;

                    case "3":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void ShowTaskMenu(List<Task> Tasks)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. View all tasks");
                Console.WriteLine("3. Edit task");
                Console.WriteLine("4. Delete task");
                Console.WriteLine("5. Mark task as completed");
                Console.WriteLine("6. Logout");
                Console.Write("Please enter your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":

                        string nameRegex = @"^[a-zA-Z]+$";
                        Console.Write("Please enter the task name: ");
                        string taskName = Console.ReadLine();
                        while (!Regex.IsMatch(taskName, nameRegex))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid task name. Please enter a name that only contains letters.");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Please enter the task name: ");
                            taskName = Console.ReadLine();
                            Console.ResetColor();
                            Console.Clear();
                        }

                        Console.Write("Enter the task description: ");
                        string description = Console.ReadLine();

                        Regex regex = new Regex("^(?=.*[a-zA-Z])[a-zA-Z0-9\\s!@#$%^&*()_+\\-={}|\\[\\]\\\\:\";'<>?,./]*$");
                        // Only allow letters or letters with numbers and at least one letter
                        while (!regex.IsMatch(description))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid description! Only letters and letters with numbers are allowed, and the description must contain at least one letter.");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Enter the task description: ");
                            Console.ResetColor();
                            description = Console.ReadLine();
                            Console.Clear();
                        }


                        Console.Write("Please enter the due date (yyyy-mm-dd): ");
                        DateTime dueDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out dueDate) || dueDate < DateTime.Now)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid date. Please enter a valid date that is in the future.");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Please enter the due date (yyyy-mm-dd): ");
                            Console.ResetColor();
                            Console.Clear();
                        }

                        string priorityString;
                        Priority priority;
                        do
                        {
                            Console.Write("Enter the task priority (low, medium, high): ");
                            priorityString = Console.ReadLine();
                        }
                        while (!Enum.TryParse(priorityString, true, out priority) ||
                              (priority != Priority.Low && priority != Priority.Medium && priority != Priority.High));

                        todoList.AddTask(currentUser, taskName, description, dueDate, priority);
                        Console.WriteLine("Task added successfully!");
                        Console.WriteLine();
                        break;

                    case "2":
                        List<Task> tasks = todoList.GetAllTasks(currentUser);
                        if (tasks.Count == 0)
                        {
                            Console.WriteLine("No tasks found.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Tabledisplay.PrintTable(Tasks, currentUser);
                        }
                        break;

                    case "3":
                        tasks = todoList.GetAllTasks(currentUser);
                        if (tasks.Count == 0)
                        {
                            Console.WriteLine("No tasks found.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Write("Please enter the task number to edit: ");
                            string input = Console.ReadLine();
                            int taskIndex;

                            if (int.TryParse(input, out taskIndex))
                            {
                                if (taskIndex > 0 && taskIndex <= tasks.Count)
                                {
                                    string TaskName;
                                    do
                                    {
                                        Console.Write("Please enter the new task name: ");
                                        TaskName = Console.ReadLine();
                                    } while (string.IsNullOrWhiteSpace(TaskName) || !TaskName.All(char.IsLetter));

                                    string Description;
                                    do
                                    {
                                        Console.Write("Enter the task description: ");
                                        Description = Console.ReadLine();
                                    } while (string.IsNullOrWhiteSpace(Description) || !Description.All(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsWhiteSpace(c)));

                                    string dueDateInput;
                                    do
                                    {
                                        Console.Write("Please enter the new due date (yyyy-mm-dd): ");
                                        dueDateInput = Console.ReadLine();
                                    } while (!DateTime.TryParse(dueDateInput, out dueDate) || dueDate <= DateTime.Today);

                                    string PriorityString;
                                    do
                                    {
                                        Console.Write("Enter the task priority (low, medium, high): ");
                                        PriorityString = Console.ReadLine();
                                    } while (!Enum.TryParse(PriorityString, true, out priority) || !Enum.GetNames(typeof(Priority)).Any(x => x.ToLower() == PriorityString.ToLower()));


                                    EditTask editTask = new EditTask()
                                    {
                                        TaskIndex = taskIndex - 1,
                                        TaskName = TaskName,
                                        Description = Description,
                                        DueDate = dueDate,
                                        PriorityPriority = priority
                                    };

                                    todoList.EditTask(currentUser, editTask);
                                    Console.WriteLine("Task edited successfully!");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid task number.");
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid task number.");
                                Console.WriteLine();
                            }
                        }
                        break;

                    case "4":
                        tasks = todoList.GetAllTasks(currentUser);
                        if (tasks.Count == 0)
                        {
                            Console.WriteLine("No tasks found.");
                            Console.WriteLine();
                        }
                        while (tasks.Count > 0)
                        {
                            Console.Write("Please enter the task number to delete: ");
                            int taskIndex = int.Parse(Console.ReadLine()) - 1;
                            todoList.DeleteTask(currentUser, taskIndex);
                            Console.WriteLine("Task deleted successfully!");
                            Console.WriteLine();
                        }
                        break;


                    case "5":
                        tasks = todoList.GetAllTasks(currentUser);
                        if (tasks.Count == 0)
                        {
                            Console.WriteLine("No tasks found.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Write("Please enter the task number to mark as completed: ");
                            if (!int.TryParse(Console.ReadLine(), out int taskIndex) || taskIndex < 1 || taskIndex > tasks.Count)
                            {
                                Console.WriteLine("Invalid task number.");
                                Console.WriteLine();
                            }
                            else
                            {
                                todoList.MarkTaskAsCompleted(currentUser, taskIndex - 1);
                                Console.WriteLine("Task marked as completed successfully!");
                                Console.WriteLine();
                            }
                        }
                        break;

                    case "6":
                        currentUser = null;
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        Console.WriteLine();
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();

            }

        }
    }

}
