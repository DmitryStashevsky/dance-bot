using System;
using DanceBotShared.Common;

namespace DanceBotShared.Bot.Actors
{
    public abstract class BotActor : BaseActor, IActorNode
    {
        public static string ActorNode => ActorNodeMetadata.Bot;
    }
}

