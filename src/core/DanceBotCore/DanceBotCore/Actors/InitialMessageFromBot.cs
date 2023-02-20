using System;
using Akka.Actor;
using DanceBotShared.Core.Messages;
using DanceBotShared.Core.Actors;
using DanceBotShared.Bot.Actors;
using DanceBotShared.Common;

namespace DanceBotCore.Actors
{
	public class InitialMessageFromBot : MessageFromBotActor
    {
        public InitialMessageFromBot()
		{
            Receive<MessageContext>(x =>
            {
                var sendMessage = Context.ActorSelection(SendToUserActor.ActorLocation);
                sendMessage.Tell(new MessageFromBot
                {
                    ChatId = x.ChatId,
                    UserName = x.UserName,
                    Message = x.Message,
                    Language = x.Language,
                    ResultMessage = "FromCore"
                });
            });
        }
    }
}

 