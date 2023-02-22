using System;
using Akka.Actor;
using DanceBotShared.Common;
using DanceBotShared.Db.Actors;

namespace DanceBotShared.Db.Actors
{
	public abstract class ExecuteQueryActor : DbActor, IActorMetadata
    {
        public static string ActorName => "ExecuteQuery";

        public static string ActorLocation => $"{DbActor.ActorNode}{ActorName}";
    }
}

