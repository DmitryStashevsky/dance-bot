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
                var message = new MessageFromBot<IList<PrivateLessonSlot>>
                {
                    MessageContext = x.Context,
                    ResultMessage = x.Data,
                    Actions = new List<BotAction> { new ViewAction { Step = nameof (ViewPrivateLessonSlotActor)} }
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

