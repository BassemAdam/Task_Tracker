using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{
    internal class TaskManager
    {
        private List<Task> ListOfTasks = new List<Task>();

        enum UtilChoice
        {
            Add,
            Edit,
            Sort,
            Filter,
            Exit
        }

        //-----------------------------------------------METHODS-----------------------------------------------------------------
        #region Execute
        public void Execute()
        {

            //* Adding some example tasks to list of tasks
            ListOfTasks.Add(new Task(
                "Task 1",
                "Task 1 Description",
                new TimeSpan(5, 45, 15),
                DateTime.Now,
                Priority.High,
                new List<string> { "Task 1", "3H" },
                Status.NotCompleted));

            ListOfTasks.Add(new Task(
                "Task 2",
                "Task 2 Description",
                new TimeSpan(4, 30, 0),
                DateTime.Now,
                Priority.Medium,
                new List<string> { "Task 3", "4M" },
                Status.NotCompleted));

            ListOfTasks.Add(DuplicateTask(ListOfTasks[0]));


            //* Input
            var input = (UtilChoice)Convert.ToInt32(Console.ReadLine());

            while (input != UtilChoice.Exit)
            {
                switch (input)
                {
                    case UtilChoice.Add:
                        Console.WriteLine("Add");
                        break;
                    case UtilChoice.Edit:
                        Console.WriteLine("Edit");
                        break;
                    case UtilChoice.Sort:
                        Console.WriteLine("Sort");
                        var sorted = Sort(UtilSortBy.Duration, UtilSortOrder.Ascending);
                        sorted.ForEach(t => Console.WriteLine(t.Title));
                        break;
                    case UtilChoice.Filter:
                        Console.WriteLine("Filter");
                        var filtered = Filter(UtilFilterBy.Priority, Priority.Medium);
                        filtered.ForEach(t => Console.WriteLine(t.Title));
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }

                input = (UtilChoice)Convert.ToInt32(Console.ReadLine());
            }


            /*
            
             UI::DisplayTasksMenu()
             Input() // Sort, Filter, Add, Edit, Exit

             WHILE (!Exit):
                 IF Sort:
                     Sort()
                 IF: Filter:
                     Filter()
                 IF Add:
                     UI::AddMenu 
                     Input()
                     CreateTask()
                     UI::Confirm ? CONFIRM : CONTINUE
                 IF Edit:
                     UI::EditMenu 
                     Input()
                     EditTask()
                     UI::Confirm ? CONFIRM : CONTINUE

                 UI::DisplayTasksMenu()
                 Input()
             */
        }
        #endregion

        //-----------------------------------------------Task Creation Section 3.2 related functions------------------------------
        #region DuplicateTask
        static private Task DuplicateTask(Task task)
        {
            Task task1 = new Task(task);
            // ListOfTasks.Add(task);
            return task1;
        }
        #endregion

        //-----------------------------------------------Task Updating 3.3 Section related functions------------------------------


        //-----------------------------------------------Task Tracking 3.4 Section related functions------------------------------

        private enum UtilFilterBy
        {
            Priority,
            Deadline
        }

        private List<Task> Filter(UtilFilterBy filterBy, object criteria)
        {
            var filteredTasks = new List<Task>();

            try
            {
                switch (filterBy)
                {
                    case UtilFilterBy.Priority:
                        filteredTasks.AddRange(ListOfTasks.FindAll(t => t.Priority == (Priority)criteria));
                        break;
                    case UtilFilterBy.Deadline:
                        filteredTasks.AddRange(ListOfTasks.FindAll(t => t.Deadline == (DateTime)criteria));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(filterBy), filterBy, null);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Criteria type does not match filter type", nameof(criteria), e);
            }

            return filteredTasks;
        }

        private enum UtilSortBy
        {
            Priority,
            Deadline,
            Duration
        }

        private enum UtilSortOrder
        {
            Ascending,
            Descending
        }

        private List<Task> Sort(UtilSortBy sortBy, UtilSortOrder sortOrder)
        {
            var sorted = ListOfTasks;


            try
            {
                switch (sortBy)
                {
                    case UtilSortBy.Priority:
                        sorted.Sort((t1, t2) => sortOrder == UtilSortOrder.Ascending ? t1.Priority.CompareTo(t2.Priority) : t2.Priority.CompareTo(t1.Priority));
                        break;
                    case UtilSortBy.Deadline:
                        sorted.Sort((t1, t2) => sortOrder == UtilSortOrder.Ascending ? t1.Deadline.CompareTo(t2.Deadline) : t2.Deadline.CompareTo(t1.Deadline));
                        break;
                    case UtilSortBy.Duration:
                        sorted.Sort((t1, t2) => sortOrder == UtilSortOrder.Ascending ? t1.Duration.CompareTo(t2.Duration) : t2.Duration.CompareTo(t1.Duration));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(sortBy), sortBy, null);

                }
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while sorting");
            }

            return sorted;
        }

        //-----------------------------------------------Advanced Features 3.5 Section related functions--------------------------





    }
}
