using System;
using Akka.Actor;
using Akka.Routing;
using DanceBotShared.Bot.Actors;
using DanceBotShared.Common;
using DanceBotShared.Core.Actors;
using DanceBotShared.Core.Messages;
using Telegram.Bot;
using TelegramClient.Handlers;

namespace TelegramClient.Actors
{
	public class MessageReceive : BotActor, IActorName
    {
		public MessageReceive(IBusinessContextHandler businessContextHandler)
		{
            Receive<MessageContext>(x =>
            {
                var businessContext = businessContextHandler.GetContext(x.Message);

                var sendMessage = Context.ActorSelection(MessageFromBotActor.ActorLocation);
                sendMessage.Tell(new TelegramContext
                {
                    MessageContext = x,
                    BusinessContext = businessContext
                });
            });
        }

        public static Props Props(IBusinessContextHandler businessContextHandler)
        {
            return Akka.Actor.Props.Create(() => new MessageReceive(businessContextHandler));
        }

        public static string ActorName => nameof(MessageReceive);
    }
}

