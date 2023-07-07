using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Task_Tracker.TaskManager;

namespace Task_Tracker // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tm = new TaskManager();
            tm.Execute();

        }
    }
}