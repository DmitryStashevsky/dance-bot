using System;
using DanceBotShared.Common;
using DanceBotShared.Db.Messages.Models;

namespace DanceBotShared.Db.Messages.Queries
{
    public class GetPrivateLessonsSlotsQuery : Query<IList<PrivateLessonSlot>>
    {
        public GetPrivateLessonsSlotsQuery(MessageContext context) : base(context)
        {
        }
    }
}

