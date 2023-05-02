using System;
namespace DanceBotShared.Common
{
	public record class BusinessContext
	{
        public string Text { get; init; }
        public string Step { get; init; }
		public string EntityId { get; init; }
		public string EntityType { get; init; }
    }
}

