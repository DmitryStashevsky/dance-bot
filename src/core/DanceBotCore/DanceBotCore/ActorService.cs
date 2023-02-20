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
        private ActorSystem? actorSystem;
        private readonly IServiceProvider serviceProvider;
        private readonly IHostApplicationLifetime applicationLifetime;
        private IActorRef messageTracker;

        private readonly Config config = ConfigurationFactory.ParseString(@"
            akka {  
                actor {
                    provider = remote
                }
                remote {
                    dot-netty.tcp {
                        port = 8081
                        hostname = 0.0.0.0
                        public-hostname = localhost
                    }
                }
            }
        ");

        public ActorService(IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        {
            this.serviceProvider = serviceProvider;
            this.applicationLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var bootstrap = BootstrapSetup.Create()
                .WithConfig(config)
                .WithActorRefProvider(ProviderSelection.Remote.Instance);

            var diSetup = DependencyResolverSetup.Create(serviceProvider);
            var actorSystemSetup = bootstrap.And(diSetup);
            actorSystem = ActorSystem.Create("DanceBot", actorSystemSetup);

            messageTracker = actorSystem.ActorOf(Props.Create<InitialMessageFromBot>(), InitialMessageFromBot.ActorName);

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

