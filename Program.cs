using System;
using System.Collections.Generic;
using JonDou9000.TaskPlanner.Domain.Models;
using JonDou9000.TaskPlanner.Domain.Models.Enums;
using JonDou9000.TaskPlanner.Domain.Logic;

internal static class Program
{
    public static void Main(string[] args)
    {
        List<WorkItem> workItems = new List<WorkItem>();
        bool continueAdding = true;

        Console.WriteLine("--- Task Planner ---");

        while (continueAdding)
        {
            Console.WriteLine("\nEnter Task:");
            WorkItem item = new WorkItem();
            item.CreationDate = DateTime.Now;
            Console.Write("Title: ");
            item.Title = Console.ReadLine();
            Console.Write("Description: ");
            item.Description = Console.ReadLine();
            Console.Write("Due Date (format dd.MM.yyyy): ");
            string dateStr = Console.ReadLine();
            if (DateTime.TryParse(dateStr, out DateTime dueDate))
            {
                item.DueDate = dueDate;
            }
            else
            {
                Console.WriteLine("Mistake in data format,setting today date ");
                item.DueDate = DateTime.Now;
            }
            Console.WriteLine("Priority (None, Low, Medium, High, Urgent): ");
            string priorityStr = Console.ReadLine();
            if (Enum.TryParse(typeof(Priority), priorityStr, true, out var priority))
            {
                item.Priority = (Priority)priority;
            }
            else
            {
                item.Priority = Priority.None;
            }
            Console.WriteLine("Complexity (None, Minutes, Hours, Days, Weeks): ");
            string complexityStr = Console.ReadLine();
            if (Enum.TryParse(typeof(Complexity), complexityStr, true, out var complexity))
            {
                item.Complexity = (Complexity)complexity;
            }
            else
            {
                item.Complexity = Complexity.None;
            }

            workItems.Add(item);

            Console.Write("\nWanna add 1 more tast? (Y/N): ");
            var key = Console.ReadKey();
            if (key.Key != ConsoleKey.Y)
            {
                continueAdding = false;
            }
            Console.WriteLine();
        }
        SimpleTaskPlanner planner = new SimpleTaskPlanner();
        WorkItem[] sortedItems = planner.CreatePlan(workItems.ToArray());
        Console.WriteLine("\n----------------------------");
        Console.WriteLine("Your plan:");
        Console.WriteLine("----------------------------");

        foreach (var item in sortedItems)
        {
            Console.WriteLine(item.ToString());
        }

        Console.ReadLine();
    }
}