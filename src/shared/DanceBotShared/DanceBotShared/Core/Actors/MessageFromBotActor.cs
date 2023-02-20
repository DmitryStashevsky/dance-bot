using System;
using DanceBotShared.Common;

namespace DanceBotShared.Core.Actors
{
    public abstract class MessageFromBotActor : CoreActor, IActorMetadata
    {
        public static string ActorName => "MessageFromBot";

        public static string ActorLocation => $"{CoreActor.ActorNode}{ActorName}";
    }
}

