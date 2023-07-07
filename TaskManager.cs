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
        static public void Execute()
        {
            // //1] Testing Please be familiar with the task data members and thier format 
            // #region TestingTask

            // Console.WriteLine("\n");
            // Console.WriteLine("this is the task with default values only the title was inputted by the user");
            // Task task2 = new Task("Complete project");
            // task2.Print();


            // Console.WriteLine("\n");
            // List<string> tags = new List<string> { "tag1", "tag2", "tag3" };
            // Task task1 = new Task("Complete project", "Complete the project by the end of the week",
            //TimeSpan.FromDays(7), new DateTime(2023, 12, 31, 23, 59, 59), Priority.High, tags, Status.NotCompleted);
            // task1.Print();

            // Console.WriteLine("\n this is the duplicated Task:");
            // Task t3 = DublicateTask(task1);
            // t3.Print();
            // #endregion

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
                        break;
                    case UtilChoice.Filter:
                        Console.WriteLine("Filter");
                        break;
                    default:
                        Console.WriteLine("Error");
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
        static private Task DublicateTask(Task task)
        {
            Task task1 = new Task(task);
            // ListOfTasks.Add(task);
            return task1;
        }
        #endregion

        //-----------------------------------------------Task Updating 3.3 Section related functions------------------------------


        //-----------------------------------------------Task Tracking 3.4 Section related functions------------------------------


        //-----------------------------------------------Advanced Features 3.5 Section related functions--------------------------





    }
}
