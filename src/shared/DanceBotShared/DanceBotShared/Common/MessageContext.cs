using System;
namespace DanceBotShared.Common
{
	public record class MessageContext
	{
        public long ChatId { get; init; }
        public int? CallbackId { get; init; }
        public string UserName { get; init; }
		public string Message { get; init; }
		public string Language { get; init; }
    }
}

