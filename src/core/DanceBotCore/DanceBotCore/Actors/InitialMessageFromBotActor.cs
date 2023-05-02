using System;
using Akka.Actor;
using DanceBotShared.Core.Messages;
using DanceBotShared.Core.Actors;
using DanceBotShared.Bot.Actors;
using DanceBotShared.Common;
using DanceBotShared.Db.Actors;
using DanceBotShared.Db.Messages.Commands;
using DanceBotShared.Db.Messages.Models;
using DanceBotShared.Db.Messages.Results;
using DanceBotShared.Db.Messages.Queries;
using DanceBotCore.Factories;

namespace DanceBotCore.Actors
{
	public class InitialMessageFromBotActor : MessageFromBotActor
    {
        public InitialMessageFromBotActor(IStepFactory stepFactory)
		{
            Receive<TelegramContext>(x =>
            {
                var step = stepFactory.GetStepHandler(Context, x.BusinessContext);
                step.Tell(x);
            });
        }

        public static Props Props(IStepFactory stepFactory)
        {
            return Akka.Actor.Props.Create(() => new InitialMessageFromBotActor(stepFactory));
        }
    }
}

 