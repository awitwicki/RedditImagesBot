using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RedditImagesBot
{
    internal class PublishBotUnit
    {
        private ITelegramBotClient _botClient { get; set; }

        public PublishBotUnit(string accessToken)
        {
            _botClient = new TelegramBotClient(accessToken);
        }

        public async Task PublishMessageAsync(string messageText, string channelName)
        {
            await _botClient.SendTextMessageAsync(channelName, messageText);
        }
        public async Task PublisPhotoAsync(string photoUrl, string channelName)
        {
            await _botClient.SendPhotoAsync(channelName, photo: photoUrl, caption: $"[link full]({photoUrl})", parseMode: ParseMode.Markdown);
        }
    }
}
