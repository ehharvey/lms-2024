﻿CommandLineParser parser = new CommandLineParser();

(Noun noun, Verb verb) = parser.Parse(args);

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

switch (noun)
{
    case Noun.Invalid:
        Console.WriteLine("Invalid noun.");
        return 400; // 400 Bad Request
    case Noun.Credits:
        Credits credits = new Credits(); // initialize here to avoid unnecessary instantiation
        credits.Execute(verb);
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