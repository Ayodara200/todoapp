using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppAyoNew
{
    internal class Tabledisplay
    {
        static readonly int tableWidth = 90;
        public static void DisplayCalHeader()
        {
            string headerText = "TodoListTable";


            int headerWidth = headerText.Length + 4;
            int centerPosition = (Console.WindowWidth / 2) - (headerWidth / 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', 120));
            Console.WriteLine($"{new string(' ', centerPosition)}|  {headerText}  |");
            Console.WriteLine(new string('=', 120));
            Console.ResetColor();
        }


        public static void PrintTable(List<Task> Tasks, User currentUser)
        {

            Console.Clear();
            Console.WriteLine(CentreText("TodoListTable", tableWidth));
            PrintLine();
            PrintRow("Id", "Title", "Description", "DueDate", "Priority", "Complete");
            PrintLine();
            int id = 1;
            foreach (Task task in currentUser.Tasks)
            {

                PrintRow(id.ToString(), task.Name, task.Description,
                    task.DueDate.ToString("yyyy-MM-dd"), task.Priority.ToString(), task.Completed.ToString());
                id++;
            }

            PrintLine();

            static void PrintLine()
            {
                Console.WriteLine(new string('-', tableWidth));
            }

            static void PrintRow(params string[] columns)
            {
                int width = (tableWidth - columns.Length + 1) / columns.Length;
                string row = "|";
                foreach (string column in columns)
                {
                    row += AlignCentre(column, width) + "|";
                }
                Console.WriteLine(row);
            }

            static string AlignCentre(string text, int width)
            {
                if (string.IsNullOrEmpty(text))
                {
                    return new string(' ', width);
                }
                text = text.Length > width ? text.Substring(0, width - 3) + "" : text;
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }

            static string CentreText(string text, int width)
            {
                if (string.IsNullOrEmpty(text))
                {
                    return new string(' ', width);
                }
                int totalSpaces = width - text.Length;
                int leftSpaces = totalSpaces / 2;
                return new string(' ', leftSpaces) + text + new string(' ', totalSpaces - leftSpaces);
            }
        }
    }
}
