using System;

namespace DanceBotShared.Db.Messages.Models
{
    public abstract class Document
    {
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

