using Lms;
using Microsoft.EntityFrameworkCore;

class WorkItem : ICommand {
    LmsDbContext db;

    public WorkItem(LmsDbContext db) {
        this.db = db;
    }

    public string GetHelp() {
        return $"""
        WorkItem

        Description:
        Tracks Work Items for the program. A Work Item is something like an assignment or quiz.

        Verbs:
        - Delete: {GetHelp(Verb.Delete)}
        """;
    }

    public string GetHelp(Verb verb) {
        switch (verb) {
            case Verb.Delete:
                return "Delete a new WorkItem";
            default:
                throw new ArgumentException("Invalid Verb");
        } 
    }

    public Lms.Models.WorkItem Delete(string id) {
        int parsed_id;
        if (!int.TryParse(id, out parsed_id)) {
            throw new ArgumentException("Invalid Id -- not an integer");
        }

        var result = db.WorkItems.Find(parsed_id);

        if (result == null) {
            throw new ArgumentException("Invalid Id -- WorkItem does not exist");
        }

        db.WorkItems.Remove(result);
        db.SaveChanges();

        return result;
    }

    public void Execute(Verb verb) {
        throw new ArgumentException("Execute requires a command_args!");
    }

    public void Execute(Verb verb, string[] command_args) {
        switch (verb) {
            case Verb.Delete:
                if (command_args.Count() < 1) {
                    throw new ArgumentException("Delete requires an ID!");
                }

                var result = Delete(command_args[0]);

                Console.WriteLine("----------------");
                Console.WriteLine($"ID: {result.Id}");
                Console.WriteLine($"Title: {result.Title}");
                Console.WriteLine($"CreatedAt: {result.CreatedAt}");
                Console.WriteLine($"DueAt: {result.DueAt}");
                Console.WriteLine("----------------");
                break;
            default:
                throw new ArgumentException("Invalid Verb!!");
        }
    }
}