using CronNET;
using RedditImagesBot;

string accessToken = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN")!;
string channelName = Environment.GetEnvironmentVariable("TELEGRAM_CHANNEL")!;
string redditTopicUrl = Environment.GetEnvironmentVariable("REDDIT_TOPIC_URL")!;

if (accessToken == null)
    throw new Exception("TELEGRAM_TOKEN environment variable is not defined");

if (channelName == null)
    throw new Exception("TELEGRAM_CHANNEL environment variable is not defined");

if (redditTopicUrl == null)
    throw new Exception("REDDIT_TOPIC_URL environment variable is not defined");

void PostNewPhoto()
{
    Console.WriteLine("Start");

    // Get Top post for today image url
    string imagreUrl = RedditParser.GetTopOfTheDayPhotoUrl(redditTopicUrl).Result;

    // Create Bot instance
    PublishBotUnit bot = new PublishBotUnit(accessToken);

    // Publish image
    bot.PublisPhotoAsync(imagreUrl, channelName).Wait();

    Console.WriteLine("End");
}

CronDaemon cronDaemon = new CronDaemon();

// Every day at 19:00
cronDaemon.AddJob("0 19 * * *", PostNewPhoto);
cronDaemon.Start();

// Wait and sleep forever. Let the cron daemon run.
Thread.Sleep(-1);
