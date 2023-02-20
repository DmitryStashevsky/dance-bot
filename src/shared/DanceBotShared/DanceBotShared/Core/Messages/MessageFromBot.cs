using System;
using DanceBotShared.Common;

namespace DanceBotShared.Core.Messages
{
	public record class MessageFromBot : MessageContext
	{
        public string ResultMessage { get; init; }
	}
}

