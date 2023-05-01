using System;
using Akka.Actor;
using DanceBotShared.Common;

namespace DanceBotShared.Db.Messages.Commands
{
	public abstract class Command<T> : Command
    {
		protected Command(MessageContext context, IActorRef actorRef)
           : base (context, actorRef)
        {}
    }

    public abstract class Command
    {
        protected Command(MessageContext context, IActorRef actorRef)
        {
            Context = context;
            ActorRef = actorRef;
        }

        public MessageContext Context;
        public IActorRef ActorRef;
    }
}


