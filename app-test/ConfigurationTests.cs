namespace configuration_tests;

using Lms;
using Newtonsoft.Json;

public class ConfigurationTests {

    [Fact]
    public void JsonDeserializeTest() 
    {
        // Arrange
        var expected_api_version = "vfoo";
        var expected_print_configuration = false;
        var json = $$"""
        {
            "version":"{{expected_api_version}}" 
        }
        """;

        // Act
        var actual = JsonConvert.DeserializeObject<_Config>(json);

        // Assert
        Assert.Equal(expected_api_version, actual.Version);
        Assert.Equal(expected_print_configuration, actual.PrintConfiguration);
    }
}