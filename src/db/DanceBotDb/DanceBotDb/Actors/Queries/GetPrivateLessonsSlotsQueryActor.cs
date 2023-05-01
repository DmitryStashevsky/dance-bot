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
	public class GetPrivateLessonsSlotsQueryActor : DbActor
	{
		public GetPrivateLessonsSlotsQueryActor(IDbContext dbContext)
		{
            ReceiveAsync<GetPrivateLessonsSlotsQuery>(async x =>
            {
                var result = await dbContext.GetAll<PrivateLessonSlot>();
                x.ActorRef.Tell(new QueryResult<IList<PrivateLessonSlot>>(x.Context, result));

            });
        }

        public static Props Props(IDbContext dbContext)
        {
            return Akka.Actor.Props.Create(() => new GetPrivateLessonsSlotsQueryActor(dbContext));
        }
    }
}

