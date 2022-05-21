using HtmlAgilityPack;

namespace RedditParser
{
    public static class RedditParser
    {
        public static async Task<(string, string, string)> GetTopOfTheDayPhotoUrl(string topicUrl)
        {
            // Parse url for domain
            string url = $"{topicUrl}top/?t=day";
            string domain = url.Split("https://")
                .Skip(1)
                .First()
                .Split("/")
                .First();

            // Request page
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = await web.LoadFromWebAsync(url);

            HtmlNode firstPost = htmlDoc
                .DocumentNode
                .SelectSingleNode(@"//div[contains(@class, 'scroll')]");

            // Search for first post
            HtmlNode postLinkNone = firstPost
                .Descendants()
                .Where(x => x.Name == "a")
                .Select(x => x)
                .Skip(1)
                .First();

            // Get first post usl
            string postUrl = postLinkNone.Attributes
                .Where(x => x.Name == "href")
                .First()
                .Value;

            // Load post url page
            htmlDoc = web.Load($"https://{domain}{postUrl}");

            // Get post node
            HtmlNode postNode = htmlDoc
                .DocumentNode
                .SelectSingleNode(@"//div[contains(@data-test-id, 'post-content')]");

            // Get post title
            string postTitle = postNode
                .Descendants()
                .Where(x => x.Name == "h1")
                .First()
                .InnerText;

            // Get all links in post
            var urls = postNode
                .Descendants()
                .Where(x => x.Name == "a")
                .ToList();

            // Filter links to get Image url
            var imageUrl = urls
                .First(x => x.FirstChild.Name == "img")
                .Attributes["href"].Value;

            return (postUrl, postTitle, imageUrl);
        }
    }
}