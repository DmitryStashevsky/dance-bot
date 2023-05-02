using System;
using Akka.Actor;
using Akka.Configuration;
using Akka.DependencyInjection;
using DanceBotCore.Actors;
using DanceBotCore.Actors.Steps.PrivateLessons;
using DanceBotCore.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace DanceBotCore
{
	public class ActorService : IHostedService
    {
        private readonly ActorSystem actorSystem;
        private readonly IServiceProvider serviceProvider;
        private readonly IHostApplicationLifetime applicationLifetime;

        private IActorRef messageTracker;
        private IActorRef listPrivateLessonsSlots;
        private IActorRef viewPrivateLessonSlot;

        public ActorService(ActorSystem actorSystem, IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        {
            this.actorSystem = actorSystem;
            this.serviceProvider = serviceProvider;
            this.applicationLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var stepFactory = serviceProvider.GetService<IStepFactory>();

            messageTracker = actorSystem.ActorOf(InitialMessageFromBotActor.Props(stepFactory), InitialMessageFromBotActor.ActorName);

            listPrivateLessonsSlots = actorSystem.ActorOf(ListPrivateLessonsSlotsActor.Props(), nameof(ListPrivateLessonsSlotsActor));
            viewPrivateLessonSlot = actorSystem.ActorOf(ViewPrivateLessonSlotActor.Props(), nameof(ViewPrivateLessonSlotActor));

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

