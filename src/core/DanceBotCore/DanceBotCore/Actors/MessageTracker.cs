using System;
using Akka.Actor;
using DanceBotShared;

namespace DanceBotCore.Actors
{
	public class MessageTracker : ReceiveActor
    {
        public MessageTracker()
		{
            Receive<ToCore>(x =>
            {
                var sendMessage = Context.ActorSelection("akka.tcp://DanceBotTelegram@localhost:8082/user/SendMessageToUser");
                sendMessage.Tell(new SendToUser { ChatId = x.ChatId, Text = "FromCore" });
            });
        }

        protected override void PostStop()
        {
            Console.WriteLine("Stop");
        }
    }
}

 