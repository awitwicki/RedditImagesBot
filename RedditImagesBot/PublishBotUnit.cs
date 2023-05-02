using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace RedditImagesBot
{
    internal class PublishBotUnit
    {
        private ITelegramBotClient BotClient { get; set; }

        public PublishBotUnit(string accessToken)
        {
            BotClient = new TelegramBotClient(accessToken);
        }

        public async Task PublishMessageAsync(string messageText, string channelName)
        {
            await BotClient.SendTextMessageAsync(channelName, messageText);
        }
        
        public async Task PublishPhotoAsync(string photoUrl, string title, string channelName)
        {
            var keyboardMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[] {
                    InlineKeyboardButton.WithUrl("Full resolution", photoUrl),
            });

            await BotClient.SendPhotoAsync(channelName, photo: photoUrl, caption: title, parseMode: ParseMode.Markdown, replyMarkup: keyboardMarkup);
        }
    }
}
