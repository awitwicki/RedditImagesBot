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
            const string subRedditUrl = "https://www.reddit.com/r/EarthPorn";

            // Act
            (var postUrl, var title, var imageUrl) = RedditParser.RedditParser.GetTopOfTheDayPhotoUrl(subRedditUrl).Result;

            // Assert
            Assert.NotNull(imageUrl);
            Assert.NotNull(title);
            Assert.NotNull(postUrl);
        }
        
        [Fact]
        public void GetTopOfTheDayPhotoUrl_WithInvalidUrl_ShouldThrowAggregateException()
        {
            // Arrange
            const string subRedditUrl = "abc";

            // Act and Assert
            Assert.Throws<AggregateException>(() =>
                RedditParser.RedditParser.GetTopOfTheDayPhotoUrl(subRedditUrl).Result);
        }
        
        [Fact]
        public async Task GetTopOfTheDayPostUrl_WithValidUrl_ShouldReturnPostUrl()
        {
            // Arrange
            const string subRedditUrl = "https://www.reddit.com/r/EarthPorn";

            // Act
            var postUrl = await RedditParser.RedditParser.GetTopOfTheDayPostUrl(subRedditUrl);

            // Assert
            Assert.NotNull(postUrl);
        }
    }
}
