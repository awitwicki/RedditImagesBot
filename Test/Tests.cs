using RedditParser;
using Xunit;

namespace Test
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            string subRedditURL = "https://www.reddit.com/r/EarthPorn/";

            // Act
            (string postUrl, string title, string imagreUrl) = RedditParser.RedditParser.GetTopOfTheDayPhotoUrl(subRedditURL).Result;

            // Assert
            Assert.NotNull(imagreUrl);
        }
    }
}
