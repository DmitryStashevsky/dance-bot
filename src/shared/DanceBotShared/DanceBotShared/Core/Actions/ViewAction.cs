using System;
namespace DanceBotShared.Core.Actions
{
	public record class ViewAction : BotAction
    {
		public string EntityId { get; init; }
        public string EntityType { get; init; }
    }
}

