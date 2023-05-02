using System;
using Akka.Actor;
using DanceBotCore.Actors.Steps;
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
            if (string.IsNullOrEmpty(businessContext.Step))
            {
                return actorContext.ActorSelection($"/user/{nameof(InitialStepActor)}");
            }
            return actorContext.ActorSelection($"/user/{businessContext.Step}");
        }
    }
}

