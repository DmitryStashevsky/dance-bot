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
	public class ListPrivateLessonsSlotsActor : StepActor
    {
		public ListPrivateLessonsSlotsActor()
		{
            Receive<TelegramContext>(x =>
            {
                var query = Context.ActorSelection(ExecuteQueryActor.ActorLocation);
                query.Tell(new GetPrivateLessonsSlotsQuery(x.MessageContext, Self));
            });

            Receive<QueryResult<IList<PrivateLessonSlot>>>(x =>
            {
                var message = new MessageFromBot
                {
                    Text = "List of private classes slots:",
                    MessageContext = x.Context,
                    Actions = x.Data.Select(y => new BotAction
                    {
                        Text = $"Place: {y.Place}, Time: {y.Time}",
                        Step = nameof (ViewPrivateLessonSlotActor),
                        EntityId = y.Id
                    }).ToList()
                };
                SendMessageToUser(message);
            });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ListPrivateLessonsSlotsActor());
        }
    }
}

