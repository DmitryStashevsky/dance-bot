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

namespace TelegramClient.Actors
{
	public class SendMessageToUser : SendToUserActor
    {
		public SendMessageToUser(ITelegramBotClient botClient)
		{
            Receive<MessageFromBot>(s =>
            {
                var inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        // first row
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("1.1", "11"),
                        },
                        // second row
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("2.1", "21"),
                        }
                    }
                );

                if (s.MessageContext.CallbackId.HasValue)
                {
                    botClient.EditMessageTextAsync(
                        chatId: s.MessageContext.ChatId,
                        messageId: s.MessageContext.CallbackId.Value,
                        replyMarkup: inlineKeyboard,
                        text: "Received");
                }
                else
                {
                    botClient.SendTextMessageAsync(
                        chatId: s.MessageContext.ChatId,
                        replyMarkup: inlineKeyboard,
                        text: "Received");
                }
            });
        }

        public static Props Props(ITelegramBotClient botClient)
        {
            return Akka.Actor.Props.Create(() => new SendMessageToUser(botClient));
        }
    }
}

