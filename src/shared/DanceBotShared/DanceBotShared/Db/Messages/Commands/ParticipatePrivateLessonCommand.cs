using System;
using Akka.Actor;
using DanceBotShared.Common;
using DanceBotShared.Db.Messages.Models;

namespace DanceBotShared.Db.Messages.Commands
{
	public class ParticipatePrivateLessonCommand : Command<PrivateLessonSlot>
    {
		public string SlotId { get; }
		public string DancerUsername { get; }

        public ParticipatePrivateLessonCommand(MessageContext context, IActorRef actorRef, string slotId, string dancerUsername)
            : base (context, actorRef)
        {
            SlotId = slotId;
            DancerUsername = dancerUsername;
        }
    }
}

