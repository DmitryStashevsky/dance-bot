using System;
using Akka.Actor;
using Akka.Configuration;
using Akka.DependencyInjection;
using DanceBotDb.Actors;
using DanceBotDb.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace DanceBotDb
{
    public class ActorService : IHostedService
    {
        private readonly ActorSystem actorSystem;
        private readonly IServiceProvider serviceProvider;
        private readonly IHostApplicationLifetime applicationLifetime;
        private IActorRef query;
        private IActorRef command;

        public ActorService(ActorSystem actorSystem, IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        {
            this.actorSystem = actorSystem;
            this.serviceProvider = serviceProvider;
            this.applicationLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var dbContext = serviceProvider.GetService<IDbContext>();

            query = actorSystem.ActorOf(QueryActor.Props(dbContext), QueryActor.ActorName);
            command = actorSystem.ActorOf(CommandActor.Props(dbContext), CommandActor.ActorName);

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

