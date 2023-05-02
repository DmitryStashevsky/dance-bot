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
		public QueryActor()
		{
            ReceiveAsync<Query>(async x =>
            {
                var query = Context.ActorSelection($"/user/{x.GetType().Name}Actor");
                query.Tell(x);
            });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new QueryActor());
        }
    }
}

