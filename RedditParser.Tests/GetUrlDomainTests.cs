using Xunit;

namespace RedditParserTests;

public class GetUrlDomainTests
{
    [InlineData("www.reddit.com/r/EarthPorn/comments/134oyn2/moody_morning_austria_1920x1280_oc/")]
    [InlineData("https://www.reddit.com/r/EarthPorn/comments/134oyn2/moody_morning_austria_1920x1280_oc/")]
    [Theory]
    public void GetUrlDomain_WithValidUrl_ShouldReturnNotNull(string url)
    {
        // Arrange

        // Act
        var domain = RedditParser.RedditParser.GetUrlDomain(url);

        // Assert
        Assert.NotNull(domain);
    }
}
