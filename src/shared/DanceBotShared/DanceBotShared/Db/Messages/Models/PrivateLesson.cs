using System;
namespace DanceBotShared.Db.Messages.Models
{
    public class PrivateLesson : Document
    {
        public long ChatId { get; set; }
        public string DancerUsername { get; set; }
        public string SlotId { get; set; }
        public string Place { get; set; }
        public DateTime Time { get; set; }
        public Status Status { get; set; }
    }
}

