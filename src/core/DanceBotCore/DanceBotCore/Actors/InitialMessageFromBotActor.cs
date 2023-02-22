using System;
using Akka.Actor;
using DanceBotShared.Core.Messages;
using DanceBotShared.Core.Actors;
using DanceBotShared.Bot.Actors;
using DanceBotShared.Common;
using DanceBotShared.Db.Actors;
using DanceBotShared.Db.Messages.Commands;
using DanceBotShared.Db.Messages.Models;
using DanceBotShared.Db.Messages.Results;
using DanceBotShared.Db.Messages.Queries;

namespace DanceBotCore.Actors
{
	public class InitialMessageFromBotActor : MessageFromBotActor
    {
        public InitialMessageFromBotActor()
		{
            Receive<MessageContext>(x =>
            {
                var addPrivateLessonSlot = Context.ActorSelection(ExecuteCommandActor.ActorLocation);
                addPrivateLessonSlot.Tell(new AddPrivateLessonSlotCommand(x, "D43", DateTime.Now));

            });

            Receive<CommandResult<PrivateLessonSlot>>(x =>
            {
                var getPrivateLessonSlot = Context.ActorSelection(ExecuteQueryActor.ActorLocation);
                getPrivateLessonSlot.Tell(new GetPrivateLessonSlotQuery(x.Context, x.Data.Id));
            });

            Receive<QueryResult<PrivateLessonSlot>>(x =>
            {
                var sendMessage = Context.ActorSelection(SendToUserActor.ActorLocation);
                sendMessage.Tell(new MessageFromBot
                {
                    ChatId = x.Context.ChatId,
                    UserName = x.Context.UserName,
                    Message = x.Context.Message,
                    Language = x.Context.Language,
                    ResultMessage = $"Id: {x.Data.Id}; Place: {x.Data.Place}; Time: {x.Data.Time}"
                });
            });
        }
    }
}

 