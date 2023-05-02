using System;
using DanceBotShared.Common;
using DanceBotShared.Core.Actions;

namespace DanceBotShared.Core.Messages
{
	public record class MessageFromBot
	{
		public string Text { get; init; }
		public MessageContext MessageContext { get; init; }
        public IList<BotAction> Actions { get; init; }
	}
}

