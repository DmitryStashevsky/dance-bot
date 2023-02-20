using System;
using DanceBotShared.Common;

namespace DanceBotShared.Bot.Actors
{
    public abstract class SendToUserActor : BotActor, IActorMetadata
    {
        public static string ActorName => "SendToUser";

        public static string ActorLocation => $"{BotActor.ActorNode}{ActorName}";
    }
}

