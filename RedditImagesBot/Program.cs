using RedditImagesBot;

Console.WriteLine("Start");

string accessToken = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN")!;
string channelName = Environment.GetEnvironmentVariable("TELEGRAM_CHANNEL")!;
string redditTopicUrl = Environment.GetEnvironmentVariable("REDDIT_TOPIC_URL")!;

if (accessToken == null)
    throw new Exception("TELEGRAM_TOKEN environment variable is not defined");

if (channelName == null)
    throw new Exception("TELEGRAM_CHANNEL environment variable is not defined");

if (redditTopicUrl == null)
    throw new Exception("REDDIT_TOPIC_URL environment variable is not defined");

// Get Top post for today image url
string imagreUrl = await RedditParser.GetTopOfTheDayPhotoUrl(redditTopicUrl);

// Create Bot instance
var bot = new PublishBotUnit(accessToken);

// Publish image
await bot.PublisPhotoAsync(imagreUrl, channelName);

Console.WriteLine("End");
