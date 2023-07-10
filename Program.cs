using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Task_Tracker.TaskManager;
using Spectre.Console;

namespace Task_Tracker // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(
                    new FigletText("Task")
                        .LeftJustified()
                        .Color(Color.Blue));

            AnsiConsole.Write(
                    new FigletText("Tracker")
                        .Centered()
                        .Color(Color.Pink1));

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.Clear();

            var tm = new TaskManager();
            tm.Execute();
        }
    }
}