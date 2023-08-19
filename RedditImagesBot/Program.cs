using CronNET;
using RedditImagesBot;

Console.WriteLine("Starting");

var accessToken = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN");
var channelName = Environment.GetEnvironmentVariable("TELEGRAM_CHANNEL");
var redditTopicUrl = Environment.GetEnvironmentVariable("REDDIT_TOPIC_URL");
var cronUtcString = Environment.GetEnvironmentVariable("CRON_UTC_STRING");

if (string.IsNullOrEmpty(accessToken))
{
    throw new ArgumentException("TELEGRAM_TOKEN environment variable is not defined");
}

if (string.IsNullOrEmpty(channelName))
{
    throw new ArgumentException("TELEGRAM_CHANNEL environment variable is not defined");
}

if (string.IsNullOrEmpty(redditTopicUrl))
{
    throw new ArgumentException("REDDIT_TOPIC_URL environment variable is not defined");
}

if (string.IsNullOrEmpty(cronUtcString))
{
    throw new ArgumentException("CRON_UTC_STRING environment variable is not defined");
}
else
{
    // TODO try to validate cron string
    if (!new CronSchedule().IsValid(cronUtcString))
    {
        throw new ArgumentException("CRON_UTC_STRING is defined wrong, use CRON scheme");
    }
}

Console.WriteLine("Config validated correctly");

void PostNewPhoto()
{
    try
    {
        Console.WriteLine("Started");

        // Get Top post for today image url
        Console.WriteLine($"Scrap top image url from {redditTopicUrl}");
        (var postUrl, var title, var imageUrl) = RedditParser.RedditParser.GetTopOfTheDayPhotoUrl(redditTopicUrl).Result;

        // Create Bot instance
        var bot = new PublishBotUnit(accessToken);

        // Publish image
        Console.WriteLine($"Trying to publish image by url {imageUrl}");
        
        // TODO Try 3 times (make a nice class for that with action in args)
        bot.PublishPhotoAsync(imageUrl, title, channelName).Wait();

        Console.WriteLine("End");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

var cronDaemon = new CronDaemon();

cronDaemon.AddJob(cronUtcString, PostNewPhoto);
cronDaemon.Start();

// Wait and sleep forever. Let the cron daemon run.
await Task.Delay(Timeout.Infinite);
