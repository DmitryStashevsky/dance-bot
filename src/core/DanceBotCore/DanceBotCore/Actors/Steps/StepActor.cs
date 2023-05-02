using System;
using DanceBotShared.Bot.Actors;
using DanceBotShared.Core.Actors;
using DanceBotShared.Core.Messages;

namespace DanceBotCore.Actors.Steps
{
	public abstract class StepActor : CoreActor
	{
		protected void SendMessageToUser(MessageFromBot message)
		{
            var sendMessageToUser = Context.ActorSelection(SendToUserActor.ActorLocation);
			sendMessageToUser.Tell(message);
        }
	}
}

