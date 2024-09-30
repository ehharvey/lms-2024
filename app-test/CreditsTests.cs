namespace credits_tests;

public class CreditsTests
{
    Credits credits = new Credits();

    [Fact]
    public void TestGetCreditsType()
    {
        Assert.True(typeof(string[]) == credits.GetCredits().GetType());
    }

    [Fact]
    public void TestGetCreditsLength()
    {
        Assert.Equal(5, credits.GetCredits().Length);
    }

    [Fact]
    public void TestGetCreditsContent()
    {
        Assert.Equal("Emil Harvey", credits.GetCredits()[0]);
        Assert.Equal("Parth Gajjar", credits.GetCredits()[1]);
        Assert.Equal("Boa Im", credits.GetCredits()[2]);
        Assert.Equal("Nimeshkumar Chaudhari", credits.GetCredits()[3]);
        Assert.Equal("Daphne Duong", credits.GetCredits()[4]);
    }
}