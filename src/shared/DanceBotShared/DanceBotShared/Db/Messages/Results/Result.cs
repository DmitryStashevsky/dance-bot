using System;
using DanceBotShared.Common;

namespace DanceBotShared.Db.Messages.Results
{
	public abstract class Result<T>
	{
		public MessageContext Context { get; }

        public T Data { get; }

        public Result(MessageContext context, T data)
        {
            Context = context;
            Data = data;
        }
    }
}

