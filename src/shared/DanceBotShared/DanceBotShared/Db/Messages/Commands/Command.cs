using System;
using DanceBotShared.Common;

namespace DanceBotShared.Db.Messages.Commands
{
	public abstract class Command<T>
    {
		protected Command(MessageContext context)
        {
            Context = context;
        }

        public MessageContext Context;
    }
}


