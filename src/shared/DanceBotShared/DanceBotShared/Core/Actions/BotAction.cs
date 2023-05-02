using System;
using DanceBotShared.Common;

namespace DanceBotShared.Core.Actions
{
	public record class BotAction : BusinessContext
	{
		public string Text { get; init; }
	}
}

