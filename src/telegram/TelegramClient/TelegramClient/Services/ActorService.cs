﻿using System;
using Akka.Actor;
using Akka.Configuration;
using Akka.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Telegram.Bot;
using TelegramClient.Actors;

namespace TelegramClient
{
    public class ActorService : IHostedService
    {
        private readonly ActorSystem actorSystem;
        private readonly IServiceProvider serviceProvider;
        private readonly IHostApplicationLifetime applicationLifetime;

        private IActorRef mesageReceive;
        private IActorRef sendMessageToUser;

        public ActorService(ActorSystem actorSystem, IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        {
            this.actorSystem = actorSystem;
            this.serviceProvider = serviceProvider;
            this.applicationLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            mesageReceive = actorSystem.ActorOf(Props.Create<MessageReceive>(), MessageReceive.ActorName);
            sendMessageToUser = actorSystem.ActorOf(SendMessageToUser.Props(serviceProvider.GetService<ITelegramBotClient>()), SendMessageToUser.ActorName);

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

