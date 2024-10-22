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
        Tracks Work Items for the program. A Work Item is something like an assignment or quiz.

        Verbs:
        - list: {GetHelp(Verb.List)}
        - edit: {GetHelp(Verb.Edit)}
        - create: {GetHelp(Verb.Create)}
        """;
    }

    public string GetHelp(Verb verb)
    {
        switch (verb) {
            case Verb.Create:
                return "Create a new WorkItem";
            case Verb.Edit:
                return "Edit an existing WorkItem";
            case Verb.List:
                return "List the WorkItems recorded previously.";
            default:
                throw new ArgumentException("Invalid Verb");
        }
    }

    public IEnumerable<Lms.Models.WorkItem> GetWorkItems()
    {
        return db.WorkItems.AsEnumerable();
    }

    public void Execute(Verb verb)
    {
        switch (verb) {
            case Verb.List:
                var work_items = GetWorkItems();
                Console.WriteLine("------------------------------");
                Console.WriteLine("| id | Title         | CreatedAt       | DueAt        | Description                       |");
                work_items.ToList().ForEach(
                    (wi) => {
                        Console.WriteLine($"| w{wi.Id} | {wi.Title} | {wi.CreatedAt:yyyy-MM-dd} | {wi.DueAt:yyyy-MM-dd} |");
                    }
                );
                Console.WriteLine("------------------------------");
                break;
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

    public void Execute(Verb verb, string[] command_args)
    {
        switch (verb) 
        {
            case Verb.List:
                Execute(verb);
                break;
            case Verb.Edit:
                if (command_args.Count() < 3) {
                    throw new ArgumentException("3 arguments: Id, Field, and Value!");
                }

                var edit_result = Edit(command_args[0], command_args[1], command_args[2]);

                Console.WriteLine("------------------------");
                Console.WriteLine($"Id: {edit_result.Id}");
                Console.WriteLine($"Title: {edit_result.Title}");
                Console.WriteLine($"CreatedAt: {edit_result.CreatedAt}");
                Console.WriteLine($"DueAt: {edit_result.DueAt}");
                Console.WriteLine("------------------------");

                break;
            case Verb.Create:
                if (command_args.Count() < 1)
                {
                    throw new ArgumentException("Create requires at least a Title Arg");
                }

                var title = command_args[0];
                string? due_at = command_args.ElementAtOrDefault(1);

                var create_result = Create(title, due_at);

                Console.WriteLine("----------------------");
                Console.WriteLine($"Id: {create_result.Id}");
                Console.WriteLine($"Title: {create_result.Title}");
                Console.WriteLine($"CreatedAt: {create_result.CreatedAt}");
                Console.WriteLine($"DueAt: {create_result.DueAt}");
                break;
                
            default:
                throw new ArgumentException("Invalid Verb");
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
}