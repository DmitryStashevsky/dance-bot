using System;
using DanceBotShared.Common;

namespace DanceBotShared.Db.Messages.Queries
{
	public abstract class Query<T>
	{
		protected Query(MessageContext context)
		{
			Context = context;
		}

		public MessageContext Context;
	}
}

