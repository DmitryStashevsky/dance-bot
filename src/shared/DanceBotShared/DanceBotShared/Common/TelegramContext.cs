using System;
namespace DanceBotShared.Common
{
	public record TelegramContext
	{
        public MessageContext MessageContext { get; init; }
        public BusinessContext BusinessContext { get; init; }
    }
}

