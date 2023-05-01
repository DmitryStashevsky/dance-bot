using System;
namespace DanceBotShared.Db.Messages.Models
{
	public class PrivateLessonSlot : Document
	{
		public string Place { get; set; }
		public DateTime Time { get; set; }
		public SlotStatus Status { get; set; }
    }
}

