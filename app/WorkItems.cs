using Lms;
using Lms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace lms
{
    class WorkItems : ICommand, IContainsArguements {
        
        private LmsDbContext db;

        public WorkItems()
        {
            this.db = null;
        }

        public WorkItems(LmsDbContext db)
        {
            this.db = db;
        }

        public string GetHelp()
        {
            return $"""
                Work Items

                Description:
                To Do.
                
                Verbs:
                - list : {GetHelp(Verb.List)}
                - create : {GetHelp(Verb.Create)}
                """;
        }

        public string GetHelp(Verb verb)
        {
            switch (verb)
            {
                case Verb.List:
                    return "Lists the Workitems for the program.";
                case Verb.Create:
                    return "Creates new WorkItem.";
                default:
                    throw new ArgumentException("Invalid verb.");
            }
        }

        // Generate WorkItem with Args (Placeholder Logic, Needs Change)
        public WorkItem GenerateWorkItem(string[] args)
        {
            if(args.Length < 3)
            {
                throw new ArgumentException("Invalid Arguments.");
            }

            string title = args[2];

            WorkItem workItem = new WorkItem() { Title = title };

            switch (args.Length)
            {
                case 3:
                    break;
                case 4:
                    if (DateTime.TryParse(args[3], out DateTime dueAt))
                    {
                        workItem.DueAt = dueAt;
                    }
                    else
                    {
                        workItem.Description = args[3];
                    }
                    break;
                case 5:
                    if (DateTime.TryParse(args[3], out dueAt))
                    {
                        workItem.DueAt = dueAt;
                        workItem.Description = args[4];
                    }
                    else
                    {
                        workItem.Description = args[3];
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid Arguments.");
            }

            ConsoleTable workItemTable = new ConsoleTable("Id", "Title", "CreatedAt", "DueAt", "Description");
            workItemTable.AddRow(workItem.Id, workItem.Title, workItem.CreatedAt.ToString(), workItem.DueAt.Value.ToString("yyyy-mm-dd"), workItem.Description);
            workItemTable.Write();
            return workItem;

        }

        // Display WorkItems
        public void DisplayAllWorkItems()
        {
            ConsoleTable workItemTable = new ConsoleTable("Id", "Title", "CreatedAt", "DueAt", "Description");
            foreach (WorkItem workItem in db.WorkItems)
            {
                workItemTable.AddRow(workItem.Id, workItem.Title, workItem.CreatedAt.ToString(), workItem.DueAt.Value.ToString("yyyy-mm-dd"), workItem.Description);
            }
            workItemTable.Write();
        }

        public void Execute(Verb verb)
        {
            switch (verb)
            {
                case Verb.List:
                    DisplayAllWorkItems();
                    break;
                default:
                    throw new ArgumentException("Invalid verb.");
            }
        }

        public void Execute(string[] args, Verb verb)
        {
            switch (verb)
            {
                case Verb.Create:
                    db.WorkItems.Add(GenerateWorkItem(args));
                    db.SaveChanges();
                    break;
                default:
                    throw new ArgumentException("Invalid verb.");
            }
        }
    }
}
