using System;
using Akka.Actor;
using DanceBotShared.Common;

namespace DanceBotShared.Db.Messages.Queries
{
	public abstract class Query<T> : Query
	{
		protected Query(MessageContext context, IActorRef actorRef)
            : base (context, actorRef)
		{}
	}

    public abstract class Query
    {
        protected Query(MessageContext context, IActorRef actorRef)
        {
            Context = context;
            ActorRef = actorRef;
        }

        public MessageContext Context;
        public IActorRef ActorRef;
    }
}

