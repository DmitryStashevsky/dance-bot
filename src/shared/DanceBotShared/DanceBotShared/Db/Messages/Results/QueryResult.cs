using System;
using DanceBotShared.Common;

namespace DanceBotShared.Db.Messages.Results
{
    public class QueryResult<T> : Result<T>
    {
        public QueryResult(MessageContext context, T data) : base(context, data)
        {
        }
    }
}

