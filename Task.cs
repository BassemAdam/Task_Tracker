using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{

    #region enums
    enum Status { NotCompleted, NotPursuing, Completed }
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
        public Task(string _title, string _description = "empty", TimeSpan _duration = default,
            DateTime _deadline = default, Priority _priority = Priority.Medium, List<string> _tags = null, Status _status = Status.NotCompleted)

        {
            NumberOfTasks++;
            UtilCounter++;
            uniqueIdentifier = UtilCounter;
            title = _title;
            description = _description;
            duration = _duration;
            deadline = _deadline;
            priority = _priority;
            tags = _tags;
            status = _status;
        }
        #endregion

        #region CopyConstructor
        public Task(Task task)

        {
            NumberOfTasks++;
            UtilCounter++;
            uniqueIdentifier = UtilCounter;
            title = task.title;
            description = task.description;
            duration = task.duration;
            deadline = task.deadline;
            priority = task.priority;
            tags = task.tags;
            status = task.status;
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
            Console.WriteLine("Tags: ");
            if (tags != null)
            {
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
