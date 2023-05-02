using System;
using Akka.Actor;
using DanceBotCore.Factories;
using DanceBotShared.Bot.Actors;
using DanceBotShared.Common;
using DanceBotShared.Core.Actions;
using DanceBotShared.Core.Actors;
using DanceBotShared.Core.Messages;
using DanceBotShared.Db.Actors;
using DanceBotShared.Db.Messages.Commands;
using DanceBotShared.Db.Messages.Models;
using DanceBotShared.Db.Messages.Queries;
using DanceBotShared.Db.Messages.Results;

namespace DanceBotCore.Actors.Steps.PrivateLessons
{
	public class ViewPrivateLessonSlotActor : StepActor
    {
		public ViewPrivateLessonSlotActor()
		{
            Receive<TelegramContext>(x =>
            {
                var query = Context.ActorSelection(ExecuteQueryActor.ActorLocation);
                query.Tell(new GetPrivateLessonSlotQuery(x.MessageContext, Self, x.BusinessContext.EntityId));
            });

            Receive<QueryResult<PrivateLessonSlot>>(x =>
            {
                var message = new MessageFromBot
                {
                    Text = $"{x.Data.Place}",
                    MessageContext = x.Context,
                    Actions = new List<BotAction>
                    {
                        new BotAction
                        {
                            Text = "Join",
                            Step = "N/A"
                        }
                    }
                };
                SendMessageToUser(message);
            });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ViewPrivateLessonSlotActor());
        }
    }
}

