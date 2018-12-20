using Bot.Bot.Replies.Interfaces;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Bot.Replies
{
    class Renderer : IResponseRenderer
    {
        private readonly ITelegramBotClient _bot;
        public Renderer(ITelegramBotClient client)
        {
            _bot = client;
        }

        public void RenderImage(IImageReply reply, int chatId)
        {
            throw new NotImplementedException();
        }

        public async void RenderMenu(IMenuReply reply, int chatId)
        {
            await _bot.SendTextMessageAsync(
                chatId: new ChatId(chatId),
                text: reply.Text,
                replyMarkup: reply.Markup,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                );
        }

        public void RenderPlainText(ITextReply reply, int chatId)
        {
            throw new NotImplementedException();
        }
    }
}
