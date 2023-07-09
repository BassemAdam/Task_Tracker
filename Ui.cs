using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{
    internal class Ui
    {
        public static string displayTaskMenu(List<Task> listOfTasks)
        {
            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.AddColumn("Title");
            table.AddColumn("Description");
            table.AddColumn("Duration");
            table.AddColumn("Deadline");
            table.AddColumn("Priority");
            table.AddColumn("Tags");
            table.AddColumn("Status");

            foreach (var task in listOfTasks)
            {
                if (task.Status != Status.NotPursuing)
                {
                    string tags = string.Join(",", task.Tags);
                    table.AddRow(task.Title,
                                    task.Description,
                                    task.Duration == TimeSpan.Zero ? "" : task.Duration.ToString(),
                                    task.Deadline == DateTime.MinValue ? "" : task.Deadline.ToString(),
                                    task.Priority == Priority.Skip ? "" : task.Priority.ToString(),
                                    tags,
                                    task.Status.ToString());
                }
            }

            AnsiConsole.Write(table);

            var select = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Add",
                    "Update",
                    "Delete",
                    "Sort",
                    "Filter",
                    "Exit"
                }));
            return select;
        }

        public static Task displayAddMenu()
        {
            Console.Clear();
            var panel = new Panel("[pink1]Add Task[/]");
            AnsiConsole.Write(panel);

            var title = AnsiConsole.Prompt(
                new TextPrompt<string>("[blue]Title?[/]")
                .Validate(title => title.Length > 0 ? ValidationResult.Success() : ValidationResult.Error("Title cannot be empty")));
            
            var description = AnsiConsole.Prompt(
                new TextPrompt<string>("[grey][[Optional]] [/][blue]Description?[/]")
                .AllowEmpty());
            
            //guys i am so sorry for what comes next
            var durationString = AnsiConsole.Prompt(
                new TextPrompt<string>("[grey][[Optional]] [/][blue]Duration?[/]")
                .AllowEmpty()
                .Validate(duration =>
                {
                    TimeSpan timeSpan;
                    if (duration == "")
                        return ValidationResult.Success();
                    if (TimeSpan.TryParse(duration, out timeSpan))
                        return ValidationResult.Success();
                    else
                        return ValidationResult.Error("Invalid TimeSpan format.");
                }));

            TimeSpan duration;

            if (durationString != "")
                duration = TimeSpan.Parse(durationString);
            else
                duration = TimeSpan.Zero;

            var deadlineString = AnsiConsole.Prompt(
                new TextPrompt<string>("[grey][[Optional]] [/][blue]Deadline?[/]")
                .AllowEmpty()
                .Validate(deadline =>
                {
                    DateTime dateTime;
                    if (deadline == "")
                        return ValidationResult.Success();
                    if (DateTime.TryParse(deadline, out dateTime))
                        return ValidationResult.Success();
                    else
                        return ValidationResult.Error("Invalid DateTime format.");
                }));


            DateTime deadline;

            if (deadlineString != "")
                deadline = DateTime.Parse(deadlineString);
            else
                deadline = DateTime.MinValue;


            var priorityString = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[grey][[Optional]] [/][blue]Priority?[/]")
                .AddChoices(new[]
                {
                    "Skip",
                    "Low",
                    "Medium",
                    "High",
                }));
            
            var priority = (Priority)Enum.Parse(typeof(Priority), priorityString);

            List<string> tags = new List<string>();

            string tag;

            if (AnsiConsole.Confirm("Add tags?", false))
            {
                tag = AnsiConsole.Ask<string>(@"[blue]Enter Tag[/] [grey][[Enter \ to exit]][/]");

                while (tag != @"\")
                {
                    tags.Add(tag);

                    tag = AnsiConsole.Ask<string>("");
                }
            }

            return new Task(title, description, duration, deadline, priority, tags);

        }
        
        public static void displayUpdateMenu(List<Task> listOfTasks)
        {
            Console.Clear();
            var panel1 = new Panel("[pink1]Update Task[/]");
            AnsiConsole.Write(panel1);


            List<string> titles = new List<string>();
            foreach (var task in listOfTasks)
            {
                titles.Add(task.Title);
            }

            var title = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Which task would you like to update?")
                .AddChoices(titles));

            var selectedTask = listOfTasks.Find(x => x.Title == title);

            //TODO: Fix error where updating task also updates other tasks with same 
            Console.Clear();
            var panel2 = new Panel($"[pink1]Update Task: {selectedTask.Title}[/]");
            AnsiConsole.Write(panel2);

            var property = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What do you want to edit ?")
                .AddChoices("Title",
                            "Description",
                            "Duration",
                            "Deadline",
                            "Priority",
                            "Tags",
                            "Status",
                            "Exit"));

            while (property != "Exit")
            {
                switch (property)
                {
                    case "Title":
                        selectedTask.Title = AnsiConsole.Prompt(
                                             new TextPrompt<string>("[blue]New Title?[/]")
                                            .Validate(title => title.Length > 0 ? ValidationResult.Success() : ValidationResult.Error("Title cannot be empty")));
                        break;
                    case "Description":
                        selectedTask.Description =  AnsiConsole.Prompt(
                                                    new TextPrompt<string>("[blue]New Description?[/]")
                                                    .AllowEmpty());
                        break;
                    case "Duration":
                        var durationString = AnsiConsole.Prompt(
                                            new TextPrompt<string>("[blue]New Duration?[/]")
                                            .AllowEmpty()
                                            .Validate(duration =>
                                            {
                                                TimeSpan timeSpan;
                                                if (duration == "")
                                                    return ValidationResult.Success();
                                                if (TimeSpan.TryParse(duration, out timeSpan))
                                                    return ValidationResult.Success();
                                                else
                                                    return ValidationResult.Error("Invalid TimeSpan format.");
                                            }));

                        if (durationString != "")
                            selectedTask.Duration = TimeSpan.Parse(durationString);
                        else
                            selectedTask.Duration = TimeSpan.Zero;
                        break;
                    case "Deadline":
                        var deadlineString = AnsiConsole.Prompt(
                                            new TextPrompt<string>("[blue]New Deadline?[/]")
                                            .AllowEmpty()
                                            .Validate(deadline =>
                                            {
                                                DateTime dateTime;
                                                if (deadline == "")
                                                    return ValidationResult.Success();
                                                if (DateTime.TryParse(deadline, out dateTime))
                                                    return ValidationResult.Success();
                                                else
                                                    return ValidationResult.Error("Invalid DateTime format.");
                                            }));

                        if (deadlineString != "")
                            selectedTask.Deadline = DateTime.Parse(deadlineString);
                        else
                            selectedTask.Deadline = DateTime.MinValue;

                        break;
                    case "Priority":
                        var priorityString = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                            .Title("[blue]New Priority?[/]")
                                            .AddChoices(new[]
                                            {
                                                "Skip",
                                                "Low",
                                                "Medium",
                                                "High",
                                            }));

                        selectedTask.Priority = (Priority)Enum.Parse(typeof(Priority), priorityString);

                        break;
                    case "Tags":

                        List<string> tags = new List<string>();
                        foreach (string tag in selectedTask.Tags)
                        {
                            tags.Add(tag);
                        }

                        string selectedTag;

                        string newTag;

                        var tagChoice = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                            .Title("[blue]What do you want to do?[/]")
                                            .AddChoices(
                                                "Edit Tag",
                                                "Add Tag",
                                                "Remove Tag"
                                            ));

                        switch (tagChoice)
                        {


                            case "Edit Tag":

                                if (tags.Count == 0)
                                {
                                    Console.WriteLine("There are no tags to edit");
                                    break;
                                }

                                selectedTag = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                            .Title("[grey][[Optional]] [/][blue] Select Tag:[/]")
                                            .AddChoices(
                                                tags
                                            ));

                                newTag = AnsiConsole.Ask<string>("[blue]Enter New Tag[/]");

                                int tagIndex = selectedTask.Tags.IndexOf(selectedTag);
                                selectedTask.Tags.Remove(selectedTag);
                                selectedTask.Tags.Insert(tagIndex, newTag);

                                break;

                            case "Add Tag":

                                newTag = AnsiConsole.Ask<string>(@"[blue]Enter Tag[/] [grey][[Enter \ to exit]][/]");

                                while (newTag != @"\")
                                {
                                    selectedTask.Tags.Add(newTag);

                                    newTag = AnsiConsole.Ask<string>("");
                                }

                                break;

                            case "Remove Tag":

                                if (tags.Count == 0)
                                {
                                    Console.WriteLine("There are no tags to remove");
                                    break;
                                }

                                selectedTag = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                            .Title("[grey][[Optional]] [/][blue] Select Tag:[/]")
                                            .AddChoices(
                                                tags
                                            ));

                                selectedTask.Tags.Remove(selectedTag);

                                break;
                        }

                        break;
                    case "Status":
                        var StatusString = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                            .Title("[blue]New Status?[/]")
                                            .AddChoices(new[]
                                            {
                                                "Completed",
                                                "Not Completed",
                                                "Not Pursuing",
                                            }));

                        selectedTask.Status = (Status)Enum.Parse(typeof(Status), StatusString);
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }

                Console.Clear();
                property = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What do you want to edit ?")
                .AddChoices("Title",
                            "Description",
                            "Duration",
                            "Deadline",
                            "Priority",
                            "Tags",
                            "Status",
                            "Exit"));
            }
        }

        public static void displayDeleteMenu(List<Task> listOfTasks)
        {
            Console.Clear();
            var panel = new Panel("[pink1]Delete Task[/]");
            AnsiConsole.Write(panel);

            List<string> titles = new List<string>();
            foreach (var task in listOfTasks)
            {
                titles.Add(task.Title);
            }

            if(titles.Count == 0)
            {
                Console.WriteLine("There are no tasks to delete");
                return;
            }

            var title = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Which task would you like to update?")
                .AddChoices(titles));

            var selectedTask = listOfTasks.Find(x => x.Title == title);

            listOfTasks.Remove(selectedTask);
        }

        public static (string,string) displaySortMenu()
        {
            Console.Clear();
            var panel = new Panel("[pink1]Sort tasks[/]");
            AnsiConsole.Write(panel);

            var sortBy = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Sort by:[/]")
                    .AddChoices(new[] {
                        "Priority",
                        "Deadline",
                        "Duration"
                    }));

            var sortOrder = AnsiConsole.Prompt(
                     new SelectionPrompt<string>()
                    .Title("[blue]Sort order:[/]")
                    .AddChoices(new[] {
                        "Ascending",
                        "Descending"
                    }));    

            return (sortBy, sortOrder);
        }
    }
}
