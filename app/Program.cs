using System.Dynamic;
using Lms;

CommandLineParser parser = new CommandLineParser();

((Noun noun, Verb verb), string[] command_args) = parser.ParseWithArgs(args);

if (noun == Noun.Invalid)
{
    Console.WriteLine("Invalid noun.");
    return 400; // 400 Bad Request
}

if (verb == Verb.Invalid)
{
    Console.WriteLine($"""
    Invalid verb for {noun}.
    """);
    return 400;
}


var dbContext = new LmsDbContext();

switch (noun)
{
    case Noun.Invalid:
        Console.WriteLine("Invalid noun.");
        return 400; // 400 Bad Request
    case Noun.Credits:
        Credits credits = new Credits(dbContext); // initialize here to avoid unnecessary instantiation
        credits.Execute(verb);
        return 200; // 200 OK

    case Noun.Progress:
        Progresses progress = new Progresses(dbContext);
        progress.Execute(verb);
        return 200; // 200 OK

    case Noun.WorkItem:
        WorkItem work_item = new WorkItem(dbContext);
        work_item.Execute(verb, command_args);
        return 200;
    default:
        Console.WriteLine(
            $"""
            Invalid noun.
            
            Valid nouns:
            {string.Join(", ", Enum.GetNames(typeof(Noun)).Where(n => n != "Invalid"))}
            """
        );
        return 400; // 400 Bad Request
}