namespace command_line_parser_tests;

public class CommandLineParserTests
{
    CommandLineParser parser = new CommandLineParser();

    [Fact]
    public void TestParseNoun()
    {
        Assert.Equal(Noun.Credits, parser.ParseNoun("credits"));
    }

    [Fact]
    public void TestParseVerb()
    {
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
}