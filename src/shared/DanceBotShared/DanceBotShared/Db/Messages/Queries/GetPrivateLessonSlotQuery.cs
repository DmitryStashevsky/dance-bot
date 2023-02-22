using System;
using DanceBotShared.Common;
using DanceBotShared.Db.Messages.Models;

namespace DanceBotShared.Db.Messages.Queries
{
	public class GetPrivateLessonSlotQuery : Query<PrivateLessonSlot>
    {
		public string Id { get; }
		public GetPrivateLessonSlotQuery(MessageContext context, string id)
			: base(context)
		{
			Id = id;
		}
	}
}

