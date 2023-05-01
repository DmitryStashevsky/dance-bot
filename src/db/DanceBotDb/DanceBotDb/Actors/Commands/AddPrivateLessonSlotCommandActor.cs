using System;
using Akka.Actor;
using DanceBotDb.Common;
using DanceBotShared.Common;
using DanceBotShared.Db.Actors;
using DanceBotShared.Db.Messages.Commands;
using DanceBotShared.Db.Messages.Models;
using DanceBotShared.Db.Messages.Results;

namespace DanceBotDb.Actors.Commands
{
	public class AddPrivateLessonSlotCommandActor : DbActor
	{
		public AddPrivateLessonSlotCommandActor(IDbContext dbContext)
		{
            ReceiveAsync<AddPrivateLessonSlotCommand>(async x =>
            {
                var result = await dbContext.Add<PrivateLessonSlot>(new PrivateLessonSlot { Place = x.Place, Time = x.Time });
                x.ActorRef.Tell(new CommandResult<PrivateLessonSlot>(ResultStatus.Success, x.Context, result));
            });
        }

        public static Props Props(IDbContext dbContext)
        {
            return Akka.Actor.Props.Create(() => new AddPrivateLessonSlotCommandActor(dbContext));
        }
    }
}

