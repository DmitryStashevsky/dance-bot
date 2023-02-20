using System;
using Akka.Actor;
using Akka.Routing;
using DanceBotShared.Bot.Actors;
using DanceBotShared.Common;
using DanceBotShared.Core.Actors;
using DanceBotShared.Core.Messages;

namespace TelegramClient.Actors
{
	public class MessageReceive : BotActor, IActorName
    {
		public MessageReceive()
		{
            Receive<MessageContext>(x => {
                var sendMessage = Context.ActorSelection(MessageFromBotActor.ActorLocation);
                sendMessage.Tell(x);
            });
        }

        public static string ActorName => "MessageReceive";
    }
}

