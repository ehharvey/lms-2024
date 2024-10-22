using Lms;

enum Field {
    Title,
    DueAt
}

class WorkItem : ICommand {
    private LmsDbContext db;

    public WorkItem(LmsDbContext db) {
        this.db = db;
    }

    public string GetHelp() {
        return $"""
        WorkItem

        Description:
        The work item for the program.

        Verbs:
        - Edit {GetHelp(Verb.Edit)}
        """;
    }

    public string GetHelp(Verb verb) {
        switch (verb) {
            case Verb.Edit:
                return "Edit a work item.";
            default:
                throw new ArgumentException("Invalid verb.");
        } 
    }

    public Lms.Models.WorkItem Edit(string id, string field, string value) {
        int parsed_id = -1;
        
        try {
            parsed_id = int.Parse(id);
        } catch {
            throw new ArgumentException("Invalid Id -- not an integer");
        }

        var result = db.WorkItems.Find(parsed_id);

        if (result == null) {
            throw new ArgumentException("Invalid Id -- WorkItem does not exist");
        }

        Field f;
        if (!Enum.TryParse<Field>(field, out f)) {
            throw new ArgumentException("Invalid field");
        }

        switch (f) {
            case Field.Title:
                result.Title = value;
                break;
            case Field.DueAt:
                DateTime parsed_due_at;

                if (DateTime.TryParse(value, out parsed_due_at))
                {
                    result.DueAt = parsed_due_at;
                }
                else {
                    throw new ArgumentException("Invalid DueAt value provided");
                }
                break;
            default:
                throw new ArgumentException("Invalid Field");
        }

        db.WorkItems.Update(result);
        db.SaveChanges();

        return result;      
    }

    public void Execute(Verb verb) {
        throw new ArgumentException("Execute without commandArgs is invalid");
    }

    public void Execute(Verb verb, string[] command_args) {
        switch (verb) {
            case Verb.Edit:
                if (command_args.Count() < 3) {
                    throw new ArgumentException("3 arguments: Id, Field, and Value!");
                }

                var result = Edit(command_args[0], command_args[1], command_args[2]);

                Console.WriteLine("------------------------");
                Console.WriteLine($"Id: ${result.Id}");
                Console.WriteLine($"Title: ${result.Title}");
                Console.WriteLine($"CreatedAt: ${result.CreatedAt}");
                Console.WriteLine($"DueAt: ${result.DueAt}");
                Console.WriteLine("------------------------");

                break;
            default:
                throw new ArgumentException("Invalid Verb");
        }
    }
}

