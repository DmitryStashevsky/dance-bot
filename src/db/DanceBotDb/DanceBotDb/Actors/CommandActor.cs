using System;
using Akka.Actor;
using DanceBotDb.Common;
using DanceBotShared.Db.Actors;
using DanceBotShared.Db.Messages.Commands;
using DanceBotShared.Db.Messages.Models;
using DanceBotShared.Db.Messages.Queries;
using DanceBotShared.Db.Messages.Results;

namespace DanceBotDb.Actors
{
	public class CommandActor : ExecuteCommandActor
	{
        private readonly IDbContext dbContext;

        public CommandActor(IDbContext dbContext)
        {
            ReceiveAsync<AddPrivateLessonSlotCommand>(async x =>
            {
                var result = await dbContext.Add<PrivateLessonSlot>(new PrivateLessonSlot { Place = x.Place, Time = x.Time});
                Context.Sender.Tell(new CommandResult<PrivateLessonSlot>(ResultStatus.Success, x.Context, result));
            });
        }

        public static Props Props(IDbContext dbContext)
        {
            return Akka.Actor.Props.Create(() => new CommandActor(dbContext));
        }
    }
}

