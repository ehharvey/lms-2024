using System.Dynamic;
using Lms;
using Lms.Models;

// Initialize the parser
CommandLineParser parser = new CommandLineParser();

((Noun noun, Verb verb), string[] commandArgs) = parser.ParseWithArgs(args);

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

// Initialize DBContext
var dbContext = new LmsDbContext();

// Initialize UserManager
var userManager = new UserManager(dbContext);

switch (noun)
{
    case Noun.Invalid:
        Console.WriteLine("Invalid noun.");
        return 400; // 400 Bad Request
    case Noun.Credit:
        Credit credits = new Credit(dbContext); // initialize here to avoid unnecessary instantiation
        credits.Execute(verb);
	return 200;
    case Noun.Block:
        Blockers blockers = new Blockers(dbContext);
        blockers.Execute(verb, commandArgs);
        return 200; // 200 OK
    case Noun.WorkItem:
        WorkItem work_item = new WorkItem(dbContext);
        work_item.Execute(verb, commandArgs);
        return 200;
    case Noun.Progress:
        Progress progress = new Progress(dbContext);
        progress.Execute(verb, commandArgs);
        return 200;
    case Noun.Tag:
        Tag tag = new Tag(dbContext);
        tag.Execute(verb, commandArgs);
        return 200;
    case Noun.User:
        
        userManager.Execute(verb, commandArgs);
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
