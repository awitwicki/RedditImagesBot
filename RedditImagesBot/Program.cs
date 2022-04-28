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
    Console.WriteLine("Start");

    // Get Top post for today image url
    (string postUrl, string title, string imagreUrl) = RedditParser.GetTopOfTheDayPhotoUrl(redditTopicUrl).Result;

    // Create Bot instance
    PublishBotUnit bot = new PublishBotUnit(accessToken);

    // Publish image
    bot.PublisPhotoAsync(imagreUrl, title, channelName).Wait();

    Console.WriteLine("End");
}

CronDaemon cronDaemon = new CronDaemon();

cronDaemon.AddJob(cronUtcString, PostNewPhoto);
cronDaemon.Start();

// Wait and sleep forever. Let the cron daemon run.
await Task.Delay(Timeout.Infinite);
