/// <summary>
/// Represents the different nouns that can be used in the command line parser.
/// A noun is the first word in a command line argument. It represents
/// the data that the command is acting on.
/// </summary>
enum Noun
{
    Credit, // Represents the "credit" noun.
    WorkItem, // Represents work item
    Block, // Represents the "block" noun.
    Progress, // Represents Progerss item
    Tag,
    User,     // Represents the "user" noun.
    Invalid // Represents an invalid noun.
}

/// <summary>
/// Represents the different verbs that can be used in the command line parser.
/// A verb is the second word in a command line argument. It represents
/// the action that the command is performing on the Noun.
/// Not all Verbs are valid for all Nouns.
/// </summary>
enum Verb
{
    List, // Represents the "list" verb. This should list all data items of the Noun.
    Create, // Represents the "Create" verb. This should create a new data item of the Noun.
    Edit, // Represents the "Edit" verb. This should edit the existing data item of the Noun.
    Delete, // Represents the "Delete" verb. This should delete the existing data item of the Noun.
    Login, // Represents the "Login" verb. This should login the user.
    Invalid, // Represents an invalid verb.

}

interface ICommandLineParser
{
    /// <summary>
    /// Parses the command line arguments and returns the Noun and Verb
    /// that the command is acting on.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    /// <returns>The Noun and Verb that the command is acting on.</returns>
    (Noun noun, Verb verb) Parse(string[] args);

    /// <summary>
    /// Parses the command line arguments and returns the Noun and Verb
    /// that the command is acting on.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    /// <returns>The Noun and Verb that the command is acting on. Also return subsequent args</returns>
    ((Noun noun, Verb verb), string[] commandLineArgs) ParseWithArgs(string[] args);

    /// <summary>
    /// Parses the Noun from the command line arguments.
    /// </summary>
    /// <param name="noun">The Noun to parse.</param>
    /// <returns>The parsed Noun.</returns>
    /// <exception cref="ArgumentException">Thrown when the Noun is invalid.</exception>
    Noun ParseNoun(string noun);

    /// <summary>
    /// Parses the Verb from the command line arguments.
    /// </summary>
    /// <param name="verb">The Verb to parse.</param>
    /// <param name="noun">The Noun that the Verb is acting on.</param>
    /// <returns>The parsed Verb.</returns>
    /// <exception cref="ArgumentException">Thrown when the Verb is invalid for the Noun.</exception>
    /// <exception cref="ArgumentException">Thrown when the Verb is invalid.</exception>
    Verb ParseVerb(string verb, Noun noun);
}

class CommandLineParser : ICommandLineParser
{
    private readonly Dictionary<Noun, HashSet<Verb>> ValidVerbs = new Dictionary<Noun, HashSet<Verb>>
    {
        // Credit supports just the List verb.
        { Noun.Credit, new HashSet<Verb> { Verb.List } },
        { Noun.WorkItem, new HashSet<Verb> { Verb.List, Verb.Create, Verb.Edit, Verb.Delete } },
        { Noun.Block, new HashSet<Verb> { Verb.List, Verb.Create, Verb.Edit, Verb.Delete } },
        { Noun.Progress, new HashSet<Verb> { Verb.Edit, Verb.Delete} },
        { Noun.Tag, new HashSet<Verb> { Verb.Create, Verb.List, Verb.Delete }},
        { Noun.User, new HashSet<Verb> { Verb.List, Verb.Edit, Verb.Create, Verb.Delete, Verb.Login } }
        // Add more Nuons and Verbs here to support more commands.
    };

    public CommandLineParser()
    {
    }

    public CommandLineParser(Dictionary<Noun, HashSet<Verb>> validVerbs)
    {
        ValidVerbs = validVerbs;
    }

    public Noun ParseNoun(string noun)
    {
        if (Enum.TryParse<Noun>(noun, true, out Noun parsedNoun))
        {
            return parsedNoun;
        }

        return Noun.Invalid;
    }

    public Verb ParseVerb(string verb, Noun noun)
    {
        if (Enum.TryParse<Verb>(verb, true, out Verb parsedVerb))
        {
            if (ValidVerbs.ContainsKey(noun) && ValidVerbs[noun].Contains(parsedVerb))
            {
                return parsedVerb;
            }

            return Verb.Invalid;
        }

        return Verb.Invalid;
    }

    public (Noun noun, Verb verb) Parse(string[] args)
    {
        if (args.Length < 2)
        {
            return (Noun.Invalid, Verb.Invalid);
        }

        Noun noun = ParseNoun(args[0]);
        Verb verb = ParseVerb(args[1], noun);
    
        return (noun, verb);
    }

    public string[] GetCommandLineArgs(string[] args)
    {
        if (args.Length < 2)
        {
            return new string[0];
        }

        return args.Skip(2).ToArray();
    }

    public ((Noun noun, Verb verb), string[] commandLineArgs) ParseWithArgs(string[] args)
    {
        (Noun noun, Verb verb) = Parse(args);
        string[] commandLineArgs = GetCommandLineArgs(args);

        return ((noun, verb), commandLineArgs);
    }
}
