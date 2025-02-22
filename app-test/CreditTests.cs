namespace credit_tests;
using Lms.Controllers;
public class CreditTests
{
    Credit credits = new Credit();

    [Fact]
    public void TestGetCreditsType()
    {
        Assert.True(typeof(List<object>) == credits.List().GetType());
    }

    [Fact]
    public void TestGetCreditsLength()
    {
        Assert.Equal(12, credits.List().Count());
    }

    [Fact]
    public void TestGetCreditsContent()
    {
        Assert.Equivalent(new { Name = "Emil Harvey" }, credits.List()[0]);
        Assert.Equivalent(new { Name = "Parth Gajjar" }, credits.List()[1]);
        Assert.Equivalent(new { Name = "Boa Im"}, credits.List()[2]);
        Assert.Equivalent(new { Name = "Nimeshkumar Chaudhari" }, credits.List()[3]);
        Assert.Equivalent(new { Name = "Daphne Duong" }, credits.List()[4]);
        Assert.Equivalent(new { Name = "Shaik Mathar Syed" }, credits.List()[5]);
        Assert.Equivalent(new { Name = "Bharat Chauhan" }, credits.List()[6]);
        Assert.Equivalent(new { Name = "Prabhdeep Singh" }, credits.List()[7]);
        Assert.Equivalent(new { Name = "Tao Boyce" }, credits.List()[8]);
        Assert.Equivalent(new { Name = "Zumhliansang Lung Ler" }, credits.List()[9]);
        Assert.Equivalent(new { Name = "Daeseong Yu" }, credits.List()[10]);
        Assert.Equivalent(new { Name = "Tian Yang" }, credits.List()[11]);
    }
}