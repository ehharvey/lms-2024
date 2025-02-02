namespace configuration_tests;

using Lms;

public class ConfigurationTests {
    [Fact]
    public void PrintConfigurationConvertTest()
    {
        // Arrange
        var expected_print_configuration = true;

        // Act
        var actual = _Config.ParseConfigValue<bool>("PrintConfiguration", "true");

        // Assert
        Assert.Equal(expected_print_configuration, actual);
    }

    [Fact]
    public void InvalidPrintConfigurationConvertTest()
    {
        // Arrange
        // Act Assert
        Assert.Throws<FormatException>(() => {
            return _Config.ParseConfigValue<bool>("PrintConfiguration", "foobar");}
        );
    }

    [Fact]
    public void InitTest() {
        // Arrange
        var expected_print_configuration = true;

        // Act
        var actual = _Config.CreateConfig([
            Tuple.Create("PrintConfiguration", expected_print_configuration.ToString())
        ]);

        // Assert
        Assert.Equal(expected_print_configuration, actual.PrintConfiguration);
    }
}