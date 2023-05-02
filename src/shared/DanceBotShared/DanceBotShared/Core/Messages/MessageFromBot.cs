using System;
using DanceBotShared.Common;
using DanceBotShared.Core.Actions;

namespace DanceBotShared.Core.Messages
{
	public abstract record class MessageFromBot
	{
		public MessageContext MessageContext { get; init; }
        public IList<BotAction> Actions { get; init; }
	}

    public record class MessageFromBot<T> : MessageFromBot
    {
        public T ResultMessage { get; init; }
    }
}

