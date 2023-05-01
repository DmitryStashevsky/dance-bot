using System;
using Akka.Actor;
using DanceBotDb.Common;
using DanceBotShared.Common;
using DanceBotShared.Db.Actors;
using DanceBotShared.Db.Messages.Commands;
using DanceBotShared.Db.Messages.Models;
using DanceBotShared.Db.Messages.Results;

namespace DanceBotDb.Actors.Commands
{
	public class ParticipatePrivateLessonCommandActor : DbActor
    {
		public ParticipatePrivateLessonCommandActor(IDbContext dbContext)
		{
            ReceiveAsync<ParticipatePrivateLessonCommand>(async x =>
            {
                var slot = await dbContext.GetById<PrivateLessonSlot>(x.SlotId);
                var privateLesson = new PrivateLesson
                {
                    Place = slot.Place,
                    Status = DanceBotShared.Db.Messages.Models.Status.Pending,
                    DancerUsername = x.DancerUsername,
                    SlotId = x.SlotId,
                    Time = slot.Time,
                    ChatId = x.Context.ChatId
                };

                var result = await dbContext.Add<PrivateLesson>(privateLesson);
                slot.Status = SlotStatus.Taken;
                await dbContext.Replace<PrivateLessonSlot>(x.SlotId, slot);
                x.ActorRef.Tell(new CommandResult<PrivateLesson>(ResultStatus.Success, x.Context, result));
            });
        }

        public static Props Props(IDbContext dbContext)
        {
            return Akka.Actor.Props.Create(() => new ParticipatePrivateLessonCommandActor(dbContext));
        }
    }
}

