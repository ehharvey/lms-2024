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
        Assert.Equal(11, credits.GetCredits().Length);
    }

    [Fact]
    public void TestGetCreditsContent()
    {
        Assert.Equal("Emil Harvey", credits.GetCredits()[0]);
        Assert.Equal("Parth Gajjar", credits.GetCredits()[1]);
        Assert.Equal("Boa Im", credits.GetCredits()[2]);
        Assert.Equal("Nimeshkumar Chaudhari", credits.GetCredits()[3]);
        Assert.Equal("Daphne Duong", credits.GetCredits()[4]);
        Assert.Equal("Shaik Mathar Syed", credits.GetCredits()[5]);
        Assert.Equal("Bharat Chauhan", credits.GetCredits()[6]);
        Assert.Equal("Prabhdeep Singh", credits.GetCredits()[7]);
        Assert.Equal("Tao Boyce", credits.GetCredits()[8]);
        Assert.Equal("Zumhliansang Lung Ler", credits.GetCredits()[9]);
        Assert.Equal("Daeseong Yu", credits.GetCredits()[10]);
    }
}