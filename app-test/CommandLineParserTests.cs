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
    public void TestParseVerbInvalid()
    {
        Assert.Throws<ArgumentException>(() => parser.ParseVerb("invalid", Noun.Credits));
    }

    [Fact]
    public void TestParseVerbInvalidForNoun()
    {
        Assert.Throws<ArgumentException>(() => parser.ParseVerb("invalid", Noun.Credits));
    }

    [Fact]
    public void TestParse()
    {
        string[] args = { "credits", "list" };
        (Noun noun, Verb verb) = parser.Parse(args);
        Assert.Equal(Noun.Credits, noun);
        Assert.Equal(Verb.List, verb);
    }
}