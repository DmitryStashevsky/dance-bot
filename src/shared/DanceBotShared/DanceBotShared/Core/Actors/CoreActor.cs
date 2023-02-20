using System;
using DanceBotShared.Common;

namespace DanceBotShared.Core.Actors
{
	public abstract class CoreActor : BaseActor, IActorNode
    {
        public static string ActorNode => ActorNodeMetadata.Core;
    }
}

