namespace command_line_parser_tests;

public class CommandLineParserTests
{
    CommandLineParser parser = new CommandLineParser();

    [Fact]
    public void TestParseNoun()
    {
        Assert.Equal(Noun.Progress, parser.ParseNoun("progress"));
        Assert.Equal(Noun.Credits, parser.ParseNoun("credits"));
    }

    [Fact]
    public void TestParseVerb()
    {
        Assert.Equal(Verb.List, parser.ParseVerb("list", Noun.Progress));
        Assert.Equal(Verb.List, parser.ParseVerb("list", Noun.Credits));
    }

    [Fact]
    public void TestParseVerbInvalidNoun()
    {
        Assert.Equal(Verb.Invalid, parser.ParseVerb("list", Noun.Invalid));
    }

    [Fact]
    public void TestParseVerbInvalidVerb()
    {
        Assert.Equal(Verb.Invalid, parser.ParseVerb("abc", Noun.Progress));
        Assert.Equal(Verb.Invalid, parser.ParseVerb("abc", Noun.Credits));
    }

    [Fact]
    public void TestParseCredits()
    {
        
            string[] args = { "credits", "list" };
            (Noun noun, Verb verb) = parser.Parse(args);
            Assert.Equal(Noun.Credits, noun);
            Assert.Equal(Verb.List, verb);
        
      
    }

    [Fact]
    public void TestParseProgress()
    {
        string[] args = { "progress", "list" };
        (Noun noun, Verb verb) = parser.Parse(args);
        Assert.Equal(Noun.Progress, noun);
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
    public void TestCommandLineArgs()
    {
        string[] args = { "credits", "list" };
        string[] commandLineArgs = parser.GetCommandLineArgs(args);
        Assert.Equal(new string[] {}, commandLineArgs);
    }



    [Fact]
    public void TestCommandLineArgsMultiple()
    {
        string[] args = { "credits", "list", "abc" };
        string[] commandLineArgs = parser.GetCommandLineArgs(args);
        Assert.Equal(new string[] { "abc" }, commandLineArgs);
    }

    [Fact]
    public void TestCommandLineArgsInvalid()
    {
        string[] args = { "credits" };
        string[] commandLineArgs = parser.GetCommandLineArgs(args);
        Assert.Equal(new string[] {}, commandLineArgs);
    }

    [Fact]
    public void TestParseWithArgs()
    {
        string[] args = { "credits", "list", "abc" };
        ((Noun noun, Verb verb), string[] commandLineArgs) = parser.ParseWithArgs(args);
        Assert.Equal(Noun.Credits, noun);
        Assert.Equal(Verb.List, verb);
        Assert.Equal(new string[] { "abc" }, commandLineArgs);
    }


}