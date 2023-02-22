using System;
using DanceBotShared.Common;

namespace DanceBotShared.Db.Actors
{
    public abstract class DbActor : BaseActor, IActorNode
    {
        public static string ActorNode => ActorNodeMetadata.Db;
    }
}

