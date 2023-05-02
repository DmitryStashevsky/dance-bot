using System;
using Akka.Actor;
using DanceBotShared.Common;

namespace DanceBotCore.Factories
{
	public interface IStepFactory
	{
        ActorSelection GetStepHandler(IUntypedActorContext actorContext, BusinessContext businessContext);
	}

    internal class StepFactory : IStepFactory
    {
        public ActorSelection GetStepHandler(IUntypedActorContext actorContext, BusinessContext businessContext)
        {
            return actorContext.ActorSelection($"/user/{businessContext.Step}");
        }
    }
}

