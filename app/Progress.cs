using ConsoleTables;
using lms.models;
using Lms;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



class Progress : ICommand {

    // DBContext for Database Interactions (Add, Update, Remove, SaveChanges)
    private LmsDbContext db;

    // Constructor - Set DB
    public Progress(LmsDbContext db) {
        this.db = db;
    }


    // Command Documentation for CLI Application

    // Main Command Documentation
    public string GetHelp() {
        return $"""
        Progress 

        Description:
        Tracks Progress of Work Items for the program.

        Verbs:
        - list: {GetHelp(Verb.List)}
        - edit: {GetHelp(Verb.Edit)}
        - create: {GetHelp(Verb.Create)}
        - delete: {GetHelp(Verb.Delete)}
        """;
    }

    // Individual Commands with their Description
    public string GetHelp(Verb verb)
    {
        switch (verb) {
            case Verb.Create:
                return "Create a new Progress";
            case Verb.Edit:
                return "Edit an existing Progress";
            case Verb.List:
                return "List the Progress recorded previously.";
            case Verb.Delete:
                return "Delete an exising Progress";
            default:
                throw new ArgumentException("Invalid Verb");
        }
    }

    public IEnumerable<Lms.Models.Progress> GetProgresses()
    {
        return db.Progresses.AsEnumerable();
    }


    // Execute Function without additional Arguments (Ex. List) -> lms Progress List
    public void Execute(Verb verb)
    {
        switch (verb) {
            case Verb.List:
                var progresses = GetProgresses();
                ConsoleTable itemTable = new ConsoleTable("ID", "Description", "WorkItem", "CreatedAt");
                progresses.ToList().ForEach(
                    (p) => {
                        itemTable.AddRow(p.Id, p.Description, p.WorkItem.Id, p.CreatedAt.ToString("yyyy-MM-dd"));
                    }
                );
                itemTable.Write();
                break;
            default:
                throw new ArgumentException("Invalid Verb");
        }
    }

    // Not Implemented Yet

    // Create Method Implementation of WorkItem for Reference

    //public Lms.Models.WorkItem Create(string title, string? due_at) {
    //    DateTime? parsed_due_at;

    //    if (due_at != null) {
    //        parsed_due_at = DateTime.Parse(due_at);
    //    }
    //    else {
    //        parsed_due_at = null;
    //    }

    //    Lms.Models.WorkItem result = new Lms.Models.WorkItem { Title = title, DueAt = parsed_due_at };

    //    db.Add(result);

    //    db.SaveChanges();

    //    return result;
    //}



    // Overloaded Execute Function with additional Arguments (Ex. Delete, Edit, Create) -> lms Progress Delete 0, lms Progress Edit 3
    public void Execute(Verb verb, string[] command_args)
    {
        switch (verb) 
        {
            case Verb.List:
                Execute(verb);
                break;
            case Verb.Delete:
                if (command_args.Count() < 1) {
                    throw new ArgumentException("Delete requires an ID!");
                }

                var delete_result = Delete(command_args[0]);

                Console.WriteLine("----------------");
                Console.WriteLine($"ID: {delete_result.Id}");
                Console.WriteLine($"Description: {delete_result.Description}");
                Console.WriteLine($"WorkItem: {delete_result.WorkItem.Id}");
                Console.WriteLine($"CreatedAt: {delete_result.CreatedAt}");
                Console.WriteLine("----------------");
                break;
            case Verb.Edit:
                if (command_args.Count() < 3) {
                    throw new ArgumentException("3 arguments: Id, Field, and Value!");
                }

                var edit_result = Edit(command_args[0], command_args[1], command_args[2]);

                Console.WriteLine("----------------");
                Console.WriteLine($"ID: {edit_result.Id}");
                Console.WriteLine($"Description: {edit_result.Description}");
                Console.WriteLine($"WorkItem: {edit_result.WorkItem.Id}");
                Console.WriteLine($"CreatedAt: {edit_result.CreatedAt}");
                Console.WriteLine("----------------");

                break;
            // Not Impletemented Yet

            // WorkItem Create case Implementation for Reference
            //case Verb.Create:
            //    if (command_args.Count() < 1)
            //    {
            //        throw new ArgumentException("Create requires at least a Title Arg");
            //    }

            //    var title = command_args[0];
            //    string? due_at = command_args.ElementAtOrDefault(1);

            //    var create_result = Create(title, due_at);

            //    Console.WriteLine("----------------------");
            //    Console.WriteLine($"Id: {create_result.Id}");
            //    Console.WriteLine($"Title: {create_result.Title}");
            //    Console.WriteLine($"CreatedAt: {create_result.CreatedAt}");
            //    Console.WriteLine($"DueAt: {create_result.DueAt}");
            //    break;
                
            default:
                throw new ArgumentException("Invalid Verb");
        }
    }

    // Edit Progress
    public Lms.Models.Progress Edit(string id, string field, string value) {
        int parsed_id = -1;
        
        try {
            parsed_id = int.Parse(id);
        } catch {
            throw new ArgumentException("Invalid Id -- not an integer");
        }

        var result = db.Progresses.Find(parsed_id);

        if (result == null) {
            throw new ArgumentException("Invalid Id -- Progress does not exist");
        }

        Item.Field f;
        if (!Enum.TryParse<Item.Field>(field, out f)) {
            throw new ArgumentException("Invalid field");
        }

        switch (f) {
            case Item.Field.Description:
                result.Description = value;
                break;
            case Item.Field.WorkItem:
                int parsed_work_item_id;
                if (int.TryParse(value, out parsed_work_item_id))
                {
                    Lms.Models.WorkItem? workitem = db.WorkItems.Find(parsed_work_item_id);
                    if(workitem != null)
                    {
                        result.WorkItem = workitem;
                    }
                    else
                    {
                        throw new ArgumentException("WorkItem Not Found");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid WorkItem Id value provided");
                }
                break;
            default:
                throw new ArgumentException("Invalid Field");
        }

        db.Progresses.Update(result);
        db.SaveChanges();

        return result;      
    }

    // Delete Progress by Id
    public Lms.Models.Progress Delete(string id) {
        int parsed_id;
        if (!int.TryParse(id, out parsed_id)) {
            throw new ArgumentException("Invalid Id -- not an integer");
        }

        var result = db.Progresses.Find(parsed_id);

        if (result == null) {
            throw new ArgumentException("Invalid Id -- Progress does not exist");
        }

        db.Progresses.Remove(result);
        db.SaveChanges();

        return result;
    }
}