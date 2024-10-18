using Lms;
using Lms.Models;
using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Windows.Markup;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

    public void Execute(Verb verb, string[] args) {
        switch (verb) {
            // listing all blockers using ConsoleTable
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

            // creating a new block
            case Verb.Create:
                // when user enter invalid command just throw excpetion
                if(args.Length == 3 || args.Length > 4) {
                    throw new ArgumentException("Invalid options.");
                }

                Block newBlock = new Block();

                // args.Length == 4 means user enters right command
                if(args.Length == 4) {
                    // to check if the description is null
                    if(args[2] != "-") {
                        newBlock.Description = args[2];
                    }

                    // to check if the work item id is null
                    if(args[3] != "-") {
                        newBlock.WorkItems = ConvertWorkItemList(args, 3);
                    }
                }
                db.Blockers.Add(newBlock);
                db.SaveChanges();
                Console.WriteLine($"The new block with id({newBlock.Id}) has been created successfully.");
                break;
            case Verb.Edit:
                // when user enter invalid command just throw excpetion
                if(args.Length != 5) {
                    throw new ArgumentException("Invalid options.");
                }

                // when user enter invalid type for blockId
                if(!int.TryParse(args[2], out int blockId)) {
                    throw new ArgumentException("Invalid block id.");
                }

                // when user enter non-existing blockId
                Block existBlock = db.Blockers.Where(b => b.Id == blockId).FirstOrDefault();
                if(existBlock == null) {
                    throw new ArgumentException("Invalid block id.");
                }

                // to check if the description is null
                if(args[3] == "-") {
                    existBlock.Description = null;
                }else {
                    existBlock.Description = args[3];
                }

                // to check if the work item id is null
                if(args[4] == "-") {
                    existBlock.WorkItems = new List<WorkItem>();
                }else {
                    existBlock.WorkItems = ConvertWorkItemList(args, 4);
                }
                db.SaveChanges();
                Console.WriteLine($"The block with id({existBlock.Id}) has been updated successfully.");
                break;
            case Verb.Delete:
                // when user enter invalid command just throw excpetion
                if(args.Length != 3) {
                    throw new ArgumentException("Invalid options.");
                }

                // when user enter invalid type for blockId
                if(!int.TryParse(args[2], out int blockIdToRemove)) {
                    throw new ArgumentException("Invalid block id.");
                }

                // when user enter non-existing blockId
                Block blockToRemove = db.Blockers.Where(b => b.Id == blockIdToRemove).FirstOrDefault();
                if(blockToRemove == null) {
                    throw new ArgumentException("Invalid block id.");
                }

                db.Blockers.Remove(blockToRemove);
                db.SaveChanges();
                Console.WriteLine($"The block with id({blockToRemove.Id}) has been deleted successfully.");
                break;
            default:
                throw new ArgumentException("Invalid verb.");
        }
    }
    private List<WorkItem> ConvertWorkItemList(string[] args, int argumentIndex) {
        List<string> workItemIds = args[argumentIndex].Split(',').ToList();
        List<WorkItem> workItems = new List<WorkItem>();
        foreach(string workitemid in workItemIds) {
            if(int.TryParse(workitemid, out int id)) {
                WorkItem workItem = db.WorkItems.Where(w => w.Id == Convert.ToInt32(id)).FirstOrDefault();
                if(workItem != null) {
                    workItems.Add(workItem);
                }
            }
            else {
                Console.WriteLine($"Wrong work item id, {workitemid}, is entered.");
            }
        }
        return workItems;
    }
}