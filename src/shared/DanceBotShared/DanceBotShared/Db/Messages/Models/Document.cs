using System;

namespace DanceBotShared.Db.Messages.Models
{
	public abstract class Document : IDocumentType
	{
        public string Id { get; set; }
    }
}

