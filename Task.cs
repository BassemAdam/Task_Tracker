using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{

    #region enums
    enum Status { NotCompleted, NotPursuing, Completed }
    enum Priority { Skip, Low, Medium, High }
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

        #region Properties

        public int UniqueIdentifier
        {
            get => uniqueIdentifier;
            set => uniqueIdentifier = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public TimeSpan Duration
        {
            get => duration;
            set => duration = value;
        }

        public DateTime Deadline
        {
            get => deadline;
            set => deadline = value;
        }

        public Priority Priority
        {
            get => priority;
            set => priority = value;
        }

        public List<string> Tags
        {
            get => tags;
            set => tags = value;
        }

        public Status Status
        {
            get => status;
            set => status = value;
        }

        #endregion

        #region Constructor
        public Task(string _title, string _description = "empty",TimeSpan _duration = default,
            DateTime _deadline = default, Priority _priority = Priority.Medium, List<string> _tags = null, Status _status = Status.NotCompleted)

        {
            NumberOfTasks++;
            UtilCounter++;
            uniqueIdentifier = UtilCounter;
            title = _title;
            description = _description;
            deadline = _deadline;
            duration = _duration;
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
            deadline = task.deadline;
            duration = task.duration;
            priority = task.priority;
            tags = task.tags;
            status = task.status;
        }
        #endregion

        #region ToStringMethod
        public override string ToString()
        {
            return "Task ID: " + uniqueIdentifier + "\n" +
                "Title: " + title + "\n" +
                "Description: " + description + "\n" +
                "Duration: " + duration + "\n" +               
                "Deadline: " + deadline + "\n" +
                "Priority: " + priority + "\n" +
                "Tags: " + string.Join(' ',tags) + "\n" +
                "Status: " + status + "\n";
            //tags[0] will be handled later by linq
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
