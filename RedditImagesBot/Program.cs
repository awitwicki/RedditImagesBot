using CronNET;
using RedditImagesBot;

string accessToken = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN")!;
string channelName = Environment.GetEnvironmentVariable("TELEGRAM_CHANNEL")!;
string redditTopicUrl = Environment.GetEnvironmentVariable("REDDIT_TOPIC_URL")!;
string cronUtcString = Environment.GetEnvironmentVariable("CRON_UTC_STRING")!;

if (accessToken == null)
    throw new Exception("TELEGRAM_TOKEN environment variable is not defined");

if (channelName == null)
    throw new Exception("TELEGRAM_CHANNEL environment variable is not defined");

if (redditTopicUrl == null)
    throw new Exception("REDDIT_TOPIC_URL environment variable is not defined");

if (cronUtcString != null)
{
    //TODO try to parse cron string
}
else
{
    throw new Exception("CRON_UTC_STRING environment variable is not defined");
}

void PostNewPhoto()
{
    try
    {
        Console.WriteLine("Started");

        // Get Top post for today image url
        Console.WriteLine($"Scrap top image urlfrom {redditTopicUrl}");
        (string postUrl, string title, string imagreUrl) = RedditParser.RedditParser.GetTopOfTheDayPhotoUrl(redditTopicUrl).Result;

        // Create Bot instance
        PublishBotUnit bot = new PublishBotUnit(accessToken);

        // Publish image
        Console.WriteLine($"Trying to publish image by url {imagreUrl}");
        bot.PublisPhotoAsync(imagreUrl, title, channelName).Wait();

        Console.WriteLine("End");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

CronDaemon cronDaemon = new CronDaemon();

cronDaemon.AddJob(cronUtcString, PostNewPhoto);
cronDaemon.Start();

// Wait and sleep forever. Let the cron daemon run.
await Task.Delay(Timeout.Infinite);
