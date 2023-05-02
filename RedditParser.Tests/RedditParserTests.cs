using System;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace RedditParserTests
{
    public class RedditParserTests
    {
        [Fact]
        public void GetTopOfTheDayPhotoUrl_WithSubredditUrl_ShouldReturnNotNull()
        {
            // Arrange
            string subRedditURL = "https://www.reddit.com/r/EarthPorn";

            // Act
            (string postUrl, string title, string imagreUrl) = RedditParser.RedditParser.GetTopOfTheDayPhotoUrl(subRedditURL).Result;

            // Assert
            Assert.NotNull(imagreUrl);
            Assert.NotNull(title);
            Assert.NotNull(postUrl);
        }
        
        [Fact]
        public async Task GetTopOfTheDayPhotoUrl_WithInvalidUrl_ShouldThrowAggregateException()
        {
            // Arrange
            string subRedditUrl = "abc";

            // Act and Assert
            Assert.Throws<AggregateException>(() =>
                RedditParser.RedditParser.GetTopOfTheDayPhotoUrl(subRedditUrl).Result);
        }
        
        [Fact]
        public async Task GetTopOfTheDayPostUrl_WithValidUrl_ShouldReturnPostUrl()
        {
            // Arrange
            var subRedditUrl = "https://www.reddit.com/r/EarthPorn";

            // Act
            var postUrl = await RedditParser.RedditParser.GetTopOfTheDayPostUrl(subRedditUrl);

            // Assert
            Assert.NotNull(postUrl);
        }
    }
}
