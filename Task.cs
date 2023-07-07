using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{

    #region enums
    enum Status { NotCompleted, NotPursuing, Completed}
    enum Priority { Low, Medium, High }
    #endregion

    internal class Task
    {
        #region DataMembers

        static int NumberOfTasks;
        static int UtilCounter;

        private int uniqueIdentifier;
        private string title; //mandatory
        private string description;
        private TimeSpan duration;
        private DateTime deadline;  //private DateTime deadline = new DateTime(2023, 12, 31, 23, 59, 59);
        private Priority priority;
        private List<string> tags;
        private Status status;
        #endregion

        //-----------------------------------------------Methods--------------------------------------------------------------

        #region Constructor
        public Task(string title, string description = "empty", TimeSpan duration = default,
            DateTime deadline = default, Priority priority = Priority.Medium, List<string> tags = null, Status status = Status.NotCompleted)

        {
            NumberOfTasks++;
            UtilCounter++;
            this.uniqueIdentifier = UtilCounter;
            this.title = title;
            this.description = description;
            this.duration = duration;
            this.deadline = deadline;
            this.priority = priority;
            this.tags = tags;
            this.status = status;
        }
        #endregion

        #region CopyConstructor
        public Task(Task task)

        {
            NumberOfTasks++;
            UtilCounter++;
            this.uniqueIdentifier = UtilCounter;
            this.title = task.title;
            this.description = task.description;
            this.duration = task.duration;
            this.deadline = task.deadline;
            this.priority = task.priority;
            this.tags = task.tags;
            this.status = task.status;
        }
        #endregion

        #region Print
        public void Print()
        {
            Console.WriteLine("Task ID: " + uniqueIdentifier);
            Console.WriteLine("Title: " + title);
            Console.WriteLine("Description: " + description);
            Console.WriteLine("Duration: " + duration);
            Console.WriteLine("Deadline: " + deadline);
            Console.WriteLine("Priority: " + priority);
            Console.WriteLine("Tags: " );
            if (tags != null) { 
            foreach (var tag in tags)
            {
                Console.WriteLine(tag);
            }
            }
            else
            {
                Console.WriteLine("No tags");
            }
            Console.WriteLine("Status: " + status);
        }
         #endregion

    }
}
