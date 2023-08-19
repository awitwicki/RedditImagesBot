using HtmlAgilityPack;
using System.Web;

namespace RedditParser
{
    public static class RedditParser
    {
        public static async Task<(string, string, string)> GetTopOfTheDayPhotoUrl(string topicUrl)
        {
            var postUrl = await GetTopOfTheDayPostUrl(topicUrl);
            var (postTitle, imageUrl) = await GetRedditPostImageUrl(postUrl);
            
            return (postUrl, postTitle, imageUrl);
        }

        public static string GetUrlDomain(string url)
        {
            return url
                .Replace("https://" , "")
                .Replace("http://" , "")
                .Split("/")[0];
        }

        public static async Task<string> GetTopOfTheDayPostUrl(string topicUrl)
        {
            // Parse url for domain
            var url = $"{topicUrl}/top/?t=day";

            // Request page
            var web = new HtmlWeb();
            var htmlDoc = await web.LoadFromWebAsync(url);
            
            var firstPost = htmlDoc
                .DocumentNode
                .SelectSingleNode(@"//shreddit-post");

            // Search for first post
            var postLinkNone = firstPost
                .Descendants()
                .Where(x => x.Name == "a")
                .Select(x => x)
                .First();

            // Get first post usl
            var postUrl = postLinkNone.Attributes
                .First(x => x.Name == "href")
                .Value;
            
            var domain = GetUrlDomain(topicUrl);

            return $"{domain}{postUrl}";
        }

        public static async Task<(string, string)> GetRedditPostImageUrl(string postUrl)
        {
            // Load post url page
            var web = new HtmlWeb();
            var htmlDoc = web.Load($"https://{postUrl}");

            // Get post name node
            var postNameNode = htmlDoc
                .DocumentNode
                .SelectSingleNode(@"//shreddit-title");
            
            // Get post node
            var postNode = htmlDoc
                .DocumentNode
                .SelectSingleNode(@"//shreddit-post");
            
            var postTitle = postNameNode.Attributes.First(x => x.Name == "title").Value;

            var imageUrl = postNode.Attributes.First(x => x.Name == "content-href").Value;

            // URLDecode post title
            postTitle = HttpUtility.HtmlDecode(postTitle);

            return (postTitle, imageUrl);
        }
    }
}