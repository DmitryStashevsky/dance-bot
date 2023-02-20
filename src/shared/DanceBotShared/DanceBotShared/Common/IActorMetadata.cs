using System;
namespace DanceBotShared.Common
{
	public interface IActorMetadata : IActorName
	{
        public static abstract string ActorLocation { get; }
    }
}

