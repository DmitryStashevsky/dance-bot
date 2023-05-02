using System;
using System.Threading;
using Akka.Actor;
using Akka.Routing;
using DanceBotShared;
using DanceBotShared.Bot.Actors;
using DanceBotShared.Common;
using DanceBotShared.Core.Messages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramClient.Handlers;

namespace TelegramClient.Actors
{
	public class SendMessageToUser : SendToUserActor
    {
		public SendMessageToUser(ITelegramBotClient botClient, IBusinessContextHandler businessContextHandler)
		{
            Receive<MessageFromBot>(s =>
            {
                var buttons = s.Actions.Select(x =>
                {
                    return new[]
                    {
                        InlineKeyboardButton.WithCallbackData(x.Text, businessContextHandler.GetString(x))
                    };
                });

                var inlineKeyboard = new InlineKeyboardMarkup(buttons);

                if (s.MessageContext.CallbackId.HasValue)
                {
                    botClient.EditMessageTextAsync(
                        chatId: s.MessageContext.ChatId,
                        messageId: s.MessageContext.CallbackId.Value,
                        replyMarkup: inlineKeyboard,
                        text: s.Text);
                }
                else
                {
                    botClient.SendTextMessageAsync(
                        chatId: s.MessageContext.ChatId,
                        replyMarkup: inlineKeyboard,
                        text: s.Text);
                }
            });
        }

        public static Props Props(ITelegramBotClient botClient, IBusinessContextHandler businessContextHandler)
        {
            return Akka.Actor.Props.Create(() => new SendMessageToUser(botClient, businessContextHandler));
        }
    }
}

