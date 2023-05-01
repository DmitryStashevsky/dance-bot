using System;
using Akka.Actor;
using Akka.Routing;
using DanceBotShared;
using DanceBotShared.Bot.Actors;
using DanceBotShared.Common;
using DanceBotShared.Core.Messages;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramClient.Actors
{
	public class SendMessageToUser : SendToUserActor
    {
        private ITelegramBotClient botClient;

		public SendMessageToUser(ITelegramBotClient botClient)
		{
            this.botClient = botClient;

            Receive<MessageFromBot>(s => {
                botClient.SendTextMessageAsync(s.ChatId, s.ResultMessage);
            });
        }

        public static Props Props(ITelegramBotClient botClient)
        {
            return Akka.Actor.Props.Create(() => new SendMessageToUser(botClient));
        }
    }
}

