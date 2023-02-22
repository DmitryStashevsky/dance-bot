using System;
using Akka.Actor;
using DanceBotDb.Common;
using DanceBotShared.Db.Actors;
using DanceBotShared.Db.Messages.Models;
using DanceBotShared.Db.Messages.Queries;
using DanceBotShared.Db.Messages.Results;

namespace DanceBotDb.Actors
{
	public class QueryActor : ExecuteQueryActor
    {
		private readonly IDbContext dbContext;
		private IActorRef actorRef;

		public QueryActor(IDbContext dbContext)
		{
			ReceiveAsync<GetPrivateLessonsSlotsQuery>(async x =>
			{
				var result = await dbContext.GetAll<PrivateLessonSlot>();
                Context.Sender.Tell(new QueryResult<IList<PrivateLessonSlot>>(x.Context, result));

            });

            ReceiveAsync<GetPrivateLessonSlotQuery>(async x =>
            {
                var result = await dbContext.GetById<PrivateLessonSlot>(x.Id);
                Context.Sender.Tell(new QueryResult<PrivateLessonSlot>(x.Context, result));
            });
        }

        public static Props Props(IDbContext dbContext)
        {
            return Akka.Actor.Props.Create(() => new QueryActor(dbContext));
        }
    }
}

