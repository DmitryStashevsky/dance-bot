using System;
using DanceBotShared.Common;

namespace DanceBotShared.Db.Messages.Results
{
    public class CommandResult<T> : Result<T>
    {
        public ResultStatus Status { get;  }

        public CommandResult(ResultStatus status, MessageContext context, T data) : base(context, data)
        {
            Status = status;
        }
    }
}

