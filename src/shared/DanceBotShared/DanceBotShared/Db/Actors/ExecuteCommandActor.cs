using System;
using Akka.Actor;
using DanceBotShared.Common;
using DanceBotShared.Db.Actors;

namespace DanceBotShared.Db.Actors
{
	public abstract class ExecuteCommandActor : DbActor, IActorMetadata
    {
        public static string ActorName => "ExecuteCommand";

        public static string ActorLocation => $"{DbActor.ActorNode}{ActorName}";
    }
}

