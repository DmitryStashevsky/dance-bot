using System;
using Akka.Actor;
using DanceBotDb.Actors.Commands;
using DanceBotDb.Common;
using DanceBotShared.Db.Actors;
using DanceBotShared.Db.Messages.Models;
using DanceBotShared.Db.Messages.Queries;
using DanceBotShared.Db.Messages.Results;

namespace DanceBotDb.Actors.Queries
{
	public class GetPrivateLessonSlotQueryActor : DbActor
	{
		public GetPrivateLessonSlotQueryActor(IDbContext dbContext)
		{
            ReceiveAsync<GetPrivateLessonSlotQuery>(async x =>
            {
                var result = await dbContext.GetById<PrivateLessonSlot>(x.Id);
                x.ActorRef.Tell(new QueryResult<PrivateLessonSlot>(x.Context, result));
            });
        }

        public static Props Props(IDbContext dbContext)
        {
            return Akka.Actor.Props.Create(() => new GetPrivateLessonSlotQueryActor(dbContext));
        }
    }
}

