using System;
using Akka.Actor;
using DanceBotCore.Actors.Steps.PrivateLessons;
using DanceBotShared.Common;
using DanceBotShared.Core.Actions;
using DanceBotShared.Core.Messages;
using DanceBotShared.Db.Actors;
using DanceBotShared.Db.Messages.Queries;

namespace DanceBotCore.Actors.Steps
{
	public class InitialStepActor : StepActor
	{
		public InitialStepActor()
		{
            Receive<TelegramContext>(x =>
            {
                var message = new MessageFromBot
                {
                    Text = "Availabla options:",
                    MessageContext = x.MessageContext,
                    Actions = new List<BotAction>
                    {
                        new BotAction
                        {
                            Text = "Private lessons",
                            Step = nameof (ListPrivateLessonsSlotsActor)
                        }
                    }
                };
                SendMessageToUser(message);
            });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new InitialStepActor());
        }
    }
}