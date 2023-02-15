using System;
using Akka.Actor;
using Akka.Routing;
using DanceBotShared;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramClient.Actors
{
	public class SendMessageToUser : ReceiveActor
    {
        private ITelegramBotClient botClient;
		public SendMessageToUser(ITelegramBotClient botClient)
		{
            this.botClient = botClient;

            Receive<SendToUser>(s => {
                botClient.SendTextMessageAsync(s.ChatId, s.Text);
            });
        }

        public static Props Props(ITelegramBotClient botCleint)
        {
            return Akka.Actor.Props.Create(() => new SendMessageToUser(botCleint));
        }
    }
}

