using Bot.Bot.Replies.Interfaces;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Bot.Replies
{
    class Renderer : IResponseRenderer
    {
        private readonly ITelegramBotClient _bot;
        private const ParseMode _parseMode = ParseMode.Html;

        public Renderer(ITelegramBotClient client)
        {
            _bot = client;
        }

        public async void RenderImage(IImageReply reply, int chatId)
        {
            await _bot.SendPhotoAsync(
                chatId: new ChatId(chatId),
                photo: reply.ImageUrl,
                caption: reply.Caption,
                parseMode: _parseMode
               );
        }

        public async void RenderMenu(IMenuReply reply, int chatId)
        {
            await _bot.SendTextMessageAsync(
                chatId: new ChatId(chatId),
                text: reply.Text,
                replyMarkup: reply.Markup,
                parseMode: _parseMode
                );
        }

        public async void RenderPlainText(ITextReply reply, int chatId)
        {
            await _bot.SendTextMessageAsync(
               chatId: new ChatId(chatId),
               text: reply.Text,
               parseMode: _parseMode
               );
        }
    }
}
