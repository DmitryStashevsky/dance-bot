
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Akka.Actor;
using Akka.DependencyInjection;
using Akka.Hosting;
using Akka.Remote.Hosting;
using Telegram.Bot;
using TelegramClient.Configuration;
using TelegramClient.Services;
using TelegramClient.Actors;
using Akka.Configuration;
using TelegramClient;
using System;
using Akka.Actor.Setup;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<BotConfiguration>(context.Configuration.GetSection(BotConfiguration.Configuration));

        services.AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    var botConfig = sp.GetService<IOptions<BotConfiguration>>();
                    var options = new TelegramBotClientOptions(botConfig.Value.BotToken);
                    var telegramBotClient = new TelegramBotClient(options, httpClient);
                    return telegramBotClient;
                });

        var config = ConfigurationFactory.ParseString(@"
            akka {  
                actor {
                    provider = remote
                }
                remote {
                    dot-netty.tcp {
                        port = 8082
                        hostname = 0.0.0.0
                        public-hostname = localhost
                    }
                }
            }
        "
        );

        var actorSystem = ActorSystem.Create("DanceBotTelegram", config);

        services.AddSingleton(actorSystem);
        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();
        services.AddHostedService<ActorService>();
    })  
    .Build();

await host.RunAsync();

