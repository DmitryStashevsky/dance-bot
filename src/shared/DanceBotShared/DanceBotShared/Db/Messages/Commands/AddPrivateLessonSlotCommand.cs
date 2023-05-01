using System;
using Akka.Actor;
using DanceBotShared.Common;
using DanceBotShared.Db.Messages.Models;

namespace DanceBotShared.Db.Messages.Commands
{
	public class AddPrivateLessonSlotCommand : Command<PrivateLessonSlot>
	{
		public string Place { get; }
		public DateTime Time { get; }

		public AddPrivateLessonSlotCommand(MessageContext context, IActorRef actorRef, string place, DateTime time)
			: base(context, actorRef)
		{
			Place = place;
			Time = time;
		}
	}
}

