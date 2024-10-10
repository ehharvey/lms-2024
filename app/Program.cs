using Lms;

CommandLineParser parser = new CommandLineParser();

(Noun noun, Verb verb, Dictionary<Option, List<string>> options) = parser.Parse(args);

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

if (options != null && options.ContainsKey(Option.Invalid)) {
    Console.WriteLine($"""
    Invalid option.
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
    case Noun.Blockers:
        Blockers blockers = new Blockers(dbContext);
        if(args.Length == 2) {
            blockers.Execute(verb);
        }else {
            blockers.Execute(verb, options);
        }
        
        return 200; // 200 OK
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
