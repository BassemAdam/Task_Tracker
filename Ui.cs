using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    "Edit",
                    "Sort",
                    "Filter",
                    "Delete",
                    "Update",
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
    }
}
