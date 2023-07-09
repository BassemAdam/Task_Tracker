﻿using Spectre.Console;
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
                string tags = string.Join(",", task.Tags);
                table.AddRow(task.Title, task.Description, task.Duration.ToString(), task.Deadline.ToString(), task.Priority.ToString(), tags, task.Status.ToString());
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

    }
}
