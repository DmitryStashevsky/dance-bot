using System;
using Akka.Actor;
using Akka.Routing;
using DanceBotShared;

namespace TelegramClient.Actors
{
	public class MessageReceive : ReceiveActor
    {
		public MessageReceive()
		{
            Receive<long>(s => {
                var sendMessage = Context.ActorSelection("akka.tcp://DanceBotCore@localhost:8081/user/MessageTracker");
                sendMessage.Tell(new ToCore { ChatId = s, Text = "FromTelegram" });
            });
        }
	}
}

