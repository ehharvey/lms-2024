namespace command_line_parser_tests;

public class CommandLineParserTests
{
    CommandLineParser parser = new CommandLineParser();

    [Fact]
    public void TestParseNoun()
    {
        Assert.Equal(Noun.Credit, parser.ParseNoun("credit"));
    }

    [Fact]
    public void TestParseNounWithBlock()
    {
        Assert.Equal(Noun.Block, parser.ParseNoun("block"));
    }

    [Fact]
    public void TestParseVerb()
    {
        Assert.Equal(Verb.List, parser.ParseVerb("list", Noun.Credit));
    }

    [Fact]
    public void TestParseVerbInvalidNoun()
    {
        Assert.Equal(Verb.Invalid, parser.ParseVerb("list", Noun.Invalid));
    }

    [Fact]
    public void TestParseVerbInvalidVerb()
    {
        Assert.Equal(Verb.Invalid, parser.ParseVerb("abc", Noun.Credit));
    }

    [Fact]
    public void TestParseCredit()
    {
        string[] args = { "credit", "list" };
        (Noun noun, Verb verb) = parser.Parse(args);
        Assert.Equal(Noun.Credit, noun);
        Assert.Equal(Verb.List, verb);
    }

    [Fact]
    public void TestParseWorkItem()
    {
        string[] args = { "workitem", "list" };
        (Noun noun, Verb verb) = parser.Parse(args);
        Assert.Equal(Noun.WorkItem, noun);
        Assert.Equal(Verb.List, verb);
    }

    [Fact]
    public void TestParseBlock()
    {
        string[] args = { "block", "list" };
        ((Noun noun, Verb verb), string[] commandArgs) = parser.ParseWithArgs(args);
        Assert.Equal(Noun.Block, noun);
        Assert.Equal(Verb.List, verb);
    }

    [Fact]
    public void TestCommandLineArgs()
    {
        string[] args = { "credit", "list" };
        string[] commandLineArgs = parser.GetCommandLineArgs(args);
        Assert.Equal(new string[] {}, commandLineArgs);
    }

    [Fact]
    public void TestCommandLineArgsMultiple()
    {
        string[] args = { "credit", "list", "abc" };
        string[] commandLineArgs = parser.GetCommandLineArgs(args);
        Assert.Equal(new string[] { "abc" }, commandLineArgs);
    }

    [Fact]
    public void TestCommandLineArgsInvalid()
    {
        string[] args = { "credit" };
        string[] commandLineArgs = parser.GetCommandLineArgs(args);
        Assert.Equal(new string[] {}, commandLineArgs);
    }

    [Fact]
    public void TestParseWithArgs()
    {
        string[] args = { "credit", "list", "abc" };
        ((Noun noun, Verb verb), string[] commandLineArgs) = parser.ParseWithArgs(args);
        Assert.Equal(Noun.Credit, noun);
        Assert.Equal(Verb.List, verb);
        Assert.Equal(new string[] { "abc" }, commandLineArgs);
    }


}
