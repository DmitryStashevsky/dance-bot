using System;
using Akka.Actor;
using Akka.Configuration;
using Akka.DependencyInjection;
using DanceBotCore.Actors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace DanceBotCore
{
	public class ActorService : IHostedService
    {
        private ActorSystem actorSystem;
        private readonly IServiceProvider serviceProvider;
        private readonly IHostApplicationLifetime applicationLifetime;
        private IActorRef messageTracker;

        public ActorService(ActorSystem actorSystem, IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        {
            this.actorSystem = actorSystem;
            this.serviceProvider = serviceProvider;
            this.applicationLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            messageTracker = actorSystem.ActorOf(Props.Create<InitialMessageFromBotActor>(), InitialMessageFromBotActor.ActorName);

            actorSystem.WhenTerminated.ContinueWith(tr => {
                applicationLifetime.StopApplication();
            });

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await CoordinatedShutdown.Get(this.actorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance);
        }
    }
}

