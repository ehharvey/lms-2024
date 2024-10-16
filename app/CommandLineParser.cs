/// <summary>
/// Represents the different nouns that can be used in the command line parser.
/// A noun is the first word in a command line argument. It represents
/// the data that the command is acting on.
/// </summary>
enum Noun
{
    Credits, // Represents the "credits" noun.
    Blockers, // Represents the "blockers" noun.
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
    Edit, // Represents the "Create" verb. This should edit the existing data item of the Noun.
    Delete, // Represents the "Create" verb. This should delete the existing data item of the Noun.
    Invalid // Represents an invalid verb.

}

enum Option 
{
    Description,
    WorkItemId,
    BlockerId,
    Invalid
}

interface ICommandLineParser
{
    /// <summary>
    /// Parses the command line arguments and returns the Noun and Verb
    /// that the command is acting on.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    /// <returns>The Noun and Verb that the command is acting on.</returns>
    (Noun noun, Verb verb, Dictionary<Option, List<string>>? options) Parse(string[] args);

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
        { Noun.Credits, new HashSet<Verb> { Verb.List } },
        { Noun.Blockers, new HashSet<Verb> { Verb.List, Verb.Create, Verb.Edit, Verb.Delete } }
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

    public Option ParseOption(string option) {
        if (Enum.TryParse<Option>(option, true, out Option parsedOption))
        {
            return parsedOption;
        }

        return Option.Invalid;
    }

    public (Noun noun, Verb verb, Dictionary<Option, List<string>>? options) Parse(string[] args)
    {
        if (args.Length < 2)
        {
            var invalidOptions = new Dictionary<Option, List<string>>
            {
                { Option.Invalid, new List<string>() }
            };
            return (Noun.Invalid, Verb.Invalid, invalidOptions);
        }

        Noun noun = ParseNoun(args[0]);
        Verb verb = ParseVerb(args[1], noun);
        
        if(args.Length == 2) {
            return (noun, verb, null);
        }

        Dictionary<Option, List<string>> options = new Dictionary<Option, List<string>>();

        for (int i = 2; i < args.Length; i++)
        {
            if (args[i].StartsWith("--"))
            {
                Option option = ParseOption(args[i].Substring(2));
                List<string> optionValues = new List<string>();

                for (int j = i + 1; j < args.Length && !args[j].StartsWith("--"); j++)
                {
                    optionValues.Add(args[j]);
                    i = j;
                }

                options.Add(option, optionValues);
            }
        }

        return (noun, verb, options);
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
        (Noun noun, Verb verb, Dictionary<Option, List<string>> options) = Parse(args);
        string[] commandLineArgs = GetCommandLineArgs(args);

        return ((noun, verb), commandLineArgs);
    }
}
