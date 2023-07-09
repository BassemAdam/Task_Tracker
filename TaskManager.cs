using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Tracker.Exceptions;

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
            Exit,
            Delete,
            Update
        }

        enum UtilProperty
        {
            Title,
            Description,
            Duration,
            Deadline,
            Priority,
            Tags,
            Status,
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

            //teting toString Method 
            //Console.WriteLine("Testing toString Method \n" + ListOfTasks[0]);

            ListOfTasks.Add(new Task(
                "Task 2",
                "Task 2 Description",
                new TimeSpan(4, 30, 0),
                DateTime.Now,
                Priority.Medium,
                new List<string> { "Task 3", "4M" },
                Status.NotCompleted));

            ListOfTasks.Add(DuplicateTask(ListOfTasks[0]));

            var input = (UtilChoice)Convert.ToInt32(Ui.displayTaskMenu(ListOfTasks));

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
                        try
                        {
                            var sorted = Sort(UtilSortBy.Duration, UtilSortOrder.Ascending);
                            sorted.ForEach(t => Console.WriteLine(t.Title));
                        }
                        catch (UtilSortException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case UtilChoice.Filter:
                        Console.WriteLine("Filter");
                        try
                        {
                            var filtered = Filter(FilterType.Deadline, Priority.Medium);
                            filtered.ForEach(t => Console.WriteLine(t.Title));
                        }
                        catch (Exception e) when (e is UtilFilterException or UtilFilterCriteriaException)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case UtilChoice.Delete:
                        Console.WriteLine("Delete");
                        break;
                    case UtilChoice.Update:
                        Console.WriteLine("Update");
                        UpdateTask(ListOfTasks[0]);
                        Console.WriteLine("Printing Updated Task \n" + ListOfTasks[0]);
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
        #region UpdateTask
        private void UpdateTask(Task task)
        {
            Console.WriteLine("what exactly you want to edit ?");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Description");
            Console.WriteLine("3. Duration");
            Console.WriteLine("4. Deadline");
            Console.WriteLine("5. Priority");
            Console.WriteLine("6. Tags");
            Console.WriteLine("7. Status");
            Console.WriteLine("8. Exit");

            var input = (UtilProperty)Convert.ToInt32(Console.ReadLine()) - 1;
            while (input != UtilProperty.Exit)
            {
                switch (input)
                {
                    case UtilProperty.Title:
                        Console.WriteLine("Enter the new title");
                        task.Title = Console.ReadLine();
                        break;
                    case UtilProperty.Description:
                        Console.WriteLine("Enter the new description");
                        task.Description = Console.ReadLine();
                        break;
                    case UtilProperty.Duration:
                        Console.WriteLine("Enter the new duration");
                        task.Duration = TimeSpan.Parse(Console.ReadLine());
                        break;
                    case UtilProperty.Deadline:
                        Console.WriteLine("Enter the new deadline");
                        task.Deadline = DateTime.Parse(Console.ReadLine());
                        break;
                    case UtilProperty.Priority:
                        Console.WriteLine("Enter the new priority");
                        task.Priority = (Priority)Convert.ToInt32(Console.ReadLine());

                        break;

                    case UtilProperty.Tags:
                        Console.WriteLine("Which Tag do u want to change");
                        string tagToBeReplaced = Console.ReadLine();
                        int index = task.Tags.IndexOf(tagToBeReplaced);
                        task.Tags.Remove(tagToBeReplaced);
                        Console.WriteLine("whats the new tag name that you want to replace it ?");
                        task.Tags.Insert(index, Console.ReadLine());
                        break;
                    case UtilProperty.Status:
                        Console.WriteLine("Enter the new status");
                        task.Status = (Status)Convert.ToInt32(Console.ReadLine());
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }

                input = (UtilProperty)Convert.ToInt32(Console.ReadLine()) - 1;
            }









        }
        #endregion

        #region DeleteTask
        private void DeleteTask(Task task)
        {
            ListOfTasks.Remove(task);
        }
        #endregion

        #region  DesignateTask
        //optional OPTIONAL: The user should have the option to designate a task as ‘Not Pursuing’,
        //indicating that it is not complete, but not should not remain open. --------->>>>>>>>>>>>>>>>>>> we will just change the status by the setter 
        #endregion
        //-----------------------------------------------Task Tracking 3.4 Section related functions------------------------------

        private enum FilterType
        {
            Priority,
            Deadline
        }

        private List<Task> Filter(FilterType filterType, object criteria)
        {
            var filteredTasks = new List<Task>();

            try
            {
                switch (filterType)
                {
                    case FilterType.Priority:
                        if (criteria is not Priority)
                        {
                            throw new UtilFilterCriteriaException($"Criteria type {criteria?.GetType()} does not match filter type {filterType}");
                        }

                        filteredTasks.AddRange(ListOfTasks.FindAll(t => t.Priority == (Priority)criteria));
                        break;

                    case FilterType.Deadline:
                        if (criteria is not DateTime)
                        {
                            throw new UtilFilterCriteriaException($"Criteria type {criteria?.GetType()} does not match filter type {filterType}");
                        }

                        filteredTasks.AddRange(ListOfTasks.FindAll(t => t.Deadline == (DateTime)criteria));
                        break;

                    default:
                        throw new UtilFilterCriteriaException($"Unrecognized filter type: {filterType}");
                }

            }
            catch (Exception e)
            {
                if (e is UtilFilterException or UtilFilterCriteriaException)
                {
                    throw;
                }

                throw new UtilFilterException("An unknown error occurred while filtering", e);
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
                throw new UtilSortException("An unknown error occurred while sorting", e);
            }

            return sorted;
        }

        //-----------------------------------------------Advanced Features 3.5 Section related functions--------------------------





    }
}
