using Lms;
using Lms.Models;
using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Windows.Markup;

class Blockers : ICommand {
    private LmsDbContext? db;

    public Blockers() {
        this.db =  null;
    }

    public Blockers(LmsDbContext db) {
        this.db = db;
    }

    public string GetHelp() {
        return $"""
        Blockers

        Description:
        The blockers for assignments.
        Verbs:
        - list: {GetHelp(Verb.List)}
        - create: {GetHelp(Verb.Create)}
        - edit: {GetHelp(Verb.Edit)}
        - delete: {GetHelp(Verb.Delete)}
        """;
    }

    public string GetHelp(Verb verb) {
        switch (verb) {
            case Verb.List:
                return "Lists the blockers for assignments.";
            case Verb.Create:
                return "Create a new blocker for a assignment.";
            case Verb.Edit:
                return "Edit the existing blocker for the assignment.";
            case Verb.Delete:
                return "Delete the existing blocker for the assignment.";
            default:
                throw new ArgumentException("Invalid verb.");
        }
    }

    public void Execute(Verb verb) {
        switch (verb) {
            case Verb.List:
                var table = new ConsoleTable("Id", "Description", "WorkItems", "CreatedAt");

                List<Block> blockers = db.Blockers.Include(b => b.WorkItems).ToList();
                foreach (var block in blockers)
                {
                    string workItems = string.Join(", ", block.WorkItems.Select(w => w.Title));
                    table.AddRow(block.Id, block.Description, workItems, block.CreatedAt);
                }
                table.Configure(o => o.NumberAlignment = Alignment.Right).Write(Format.Alternative);
                break;
            case Verb.Create:
            case Verb.Edit:
            case Verb.Delete:
                throw new ArgumentException("No option entered.");
            default:
                throw new ArgumentException("Invalid verb.");
        }
    }
    public void Execute(Verb verb, Dictionary<Option, List<string>> options) {
        switch (verb) {
            case Verb.Create:
                // check if the option is invalid.
                if(options.ContainsKey(Option.Invalid)) {
                    throw new ArgumentException("Invalid option.");
                }

                Block newBlock = new Block();

                // if options have description, save it into new block.
                if(options.ContainsKey(Option.Description)) {
                    foreach(string des in options[Option.Description]) {
                        newBlock.Description = des;
                    }
                }

                // if options have workitem list, save them into new block.
                if(options.ContainsKey(Option.WorkItemId)) {
                    List<WorkItem> workItems = new List<WorkItem>();
                    foreach(string workitemid in options[Option.WorkItemId]) {
                        WorkItem workItem = db.WorkItems.Where(w => w.Id == Convert.ToInt32(workitemid)).FirstOrDefault();
                        workItems.Add(workItem);
                    }
                    newBlock.WorkItems = workItems;
                }
                
                db.Blockers.Add(newBlock);
                db.SaveChanges();
                break;
            case Verb.Edit:
                // check if the option is invalid.
                if(options.ContainsKey(Option.Invalid)) {
                    throw new ArgumentException("Invalid option.");
                }

                // check if the options has blocker id
                if((!options.ContainsKey(Option.BlockerId)) || (options.ContainsKey(Option.BlockerId) && options[Option.BlockerId].Count() == 0)) {
                    throw new ArgumentException("BlockerId is needed to edit the blocker.");
                }

                Block existBlock = db.Blockers.Where(b => b.Id == Convert.ToInt32(options[Option.BlockerId][0])).FirstOrDefault();
                if(existBlock == null) {
                    throw new ArgumentException("Wrong BlockerId was entered.");
                }

                // if options have description, change the block's description to new value.
                if(options.ContainsKey(Option.Description)) {
                    foreach(string des in options[Option.Description]) {
                        existBlock.Description = des;
                    }
                }

                // if options have workitem list, change the block's workitems to new value.
                if(options.ContainsKey(Option.WorkItemId)) {
                    List<WorkItem> workItems = new List<WorkItem>();
                    foreach(string workitemid in options[Option.WorkItemId]) {
                        WorkItem workItem = db.WorkItems.Where(w => w.Id == Convert.ToInt32(workitemid)).FirstOrDefault();
                        workItems.Add(workItem);
                    }
                    existBlock.WorkItems = workItems;
                }

                db.SaveChanges();
                break;
            case Verb.Delete:
                // check if the option is invalid.
                if(options.ContainsKey(Option.Invalid)) {
                    throw new ArgumentException("Invalid option.");
                }

                // check if the options has blocker id
                if((!options.ContainsKey(Option.BlockerId)) || (options.ContainsKey(Option.BlockerId) && options[Option.BlockerId].Count() == 0)) {
                    throw new ArgumentException("BlockerId is needed to delete the blocker.");
                }

                Block blockToDelete = db.Blockers.Where(b => b.Id == Convert.ToInt32(options[Option.BlockerId][0])).FirstOrDefault();
                if(blockToDelete == null) {
                    throw new ArgumentException("Wrong BlockerId was entered.");
                }

                db.Blockers.Remove(blockToDelete);
                db.SaveChanges();
                break;
            default:
                throw new ArgumentException("Invalid verb.");
        }
    }
}