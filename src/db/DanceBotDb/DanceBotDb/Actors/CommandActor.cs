using System;
using Akka.Actor;
using DanceBotDb.Actors.Commands;
using DanceBotDb.Common;
using DanceBotShared.Db.Actors;
using DanceBotShared.Db.Messages.Commands;
using DanceBotShared.Db.Messages.Models;
using DanceBotShared.Db.Messages.Queries;
using DanceBotShared.Db.Messages.Results;

namespace DanceBotDb.Actors
{
	public class CommandActor : ExecuteCommandActor
	{
        public CommandActor()
        {
            ReceiveAsync<Command>(async x =>
            {
                var command = Context.ActorSelection($"/user/{x.GetType().Name}Actor");
                command.Tell(x);
            });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new CommandActor());
        }
    }
}

