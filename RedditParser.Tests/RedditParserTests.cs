using System;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace RedditParserTests
{
    public class RedditParserTests
    {
        [Fact]
        public async Task GetTopOfTheDayPhotoUrl_WithSubredditUrl_ShouldReturnNotNull()
        {
            // Arrange
            const string subRedditUrl = "https://www.reddit.com/r/EarthPorn";

            // Act
            var (postUrl, title, imageUrl) = await RedditParser.RedditParser.GetTopOfTheDayPhotoUrl(subRedditUrl);

            // Assert
            Assert.NotNull(imageUrl);
            Assert.NotNull(title);
            Assert.NotNull(postUrl);
        }
        
        [Fact]
        public async Task GetTopOfTheDayPhotoUrl_WithInvalidUrl_ShouldThrowAggregateException()
        {
            // Arrange
            const string subRedditUrl = "https://www.reddit.com/r/EarthPornabc";

            // Act and Assert
            await Assert.ThrowsAsync<AggregateException>( async () =>
                await RedditParser.RedditParser.GetTopOfTheDayPhotoUrl(subRedditUrl));
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
