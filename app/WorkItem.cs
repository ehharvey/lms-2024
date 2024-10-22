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
        - list: {GetHelp(Verb.List)}
        """;
    }

    public string GetHelp(Verb verb)
    {
        switch (verb) {
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
                        Console.WriteLine($"| w{wi.Id} | {wi.Title} | {wi.CreatedAt:yyyy-MM-dd} | {wi.DueAt.ToString:yyyy-MM-dd} |");
                    }
                );
                Console.WriteLine("------------------------------");
                break;
            default:
                throw new ArgumentException("Invalid Verb");
        }
    }
}