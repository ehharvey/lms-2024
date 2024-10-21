using Lms;

class WorkItem : ICommand {
    private LmsDbContext db;

    public WorkItem(LmsDbContext db) {
        this.db = db;
    }

    public string GetHelp() {
        return $"""
        WorkItem

        Description:
        Tracks Work Items for the program. A Work Item is something like an assignment or quiz.
        
        Verbs:
        - Create: {GetHelp(Verb.Create)}
        """;
    }

    public string GetHelp(Verb verb) {
        switch (verb) {
            case Verb.Create:
                return "Create a new WorkItem";
            default:
                throw new ArgumentException("Invalid Verb");
        }
    }

    public Lms.Models.WorkItem Create(string title, string? due_at) {
        DateTime? parsed_due_at;

        if (due_at != null) {
            parsed_due_at = DateTime.Parse(due_at);
        }
        else {
            parsed_due_at = null;
        }

        Lms.Models.WorkItem result = new Lms.Models.WorkItem { Title = title, DueAt = parsed_due_at };

        db.Add(result);

        db.SaveChanges();

        return result;
    }

    public void Execute(Verb verb) {
        throw new ArgumentException("Execute must be called with command_args");
    }

    public void Execute(Verb verb, string[] command_args)
    {
        switch (verb) 
        {
            case Verb.Create:
                if (command_args.Count() < 1)
                {
                    throw new ArgumentException("Create requires at least a Title Arg");
                }

                var title = command_args[0];
                string? due_at = command_args.ElementAtOrDefault(1);

                var result = Create(title, due_at);

                Console.WriteLine("----------------------");
                Console.WriteLine($"Id: {result.Id}");
                Console.WriteLine($"Title: {result.Title}");
                Console.WriteLine($"CreatedAt: {result.CreatedAt}");
                Console.WriteLine($"DueAt: {result.DueAt}");
                break;
                
            default:
                throw new ArgumentException("Invalid Verb");
        }
    }
}