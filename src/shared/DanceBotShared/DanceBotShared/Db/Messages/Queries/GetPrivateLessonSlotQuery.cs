using System;
using Akka.Actor;
using DanceBotShared.Common;
using DanceBotShared.Db.Messages.Models;

namespace DanceBotShared.Db.Messages.Queries
{
	public class GetPrivateLessonSlotQuery : Query<PrivateLessonSlot>
    {
		public string Id { get; }

		public GetPrivateLessonSlotQuery(MessageContext context, IActorRef actorRef, string id)
			: base(context, actorRef)
		{
			Id = id;
		}
	}
}

