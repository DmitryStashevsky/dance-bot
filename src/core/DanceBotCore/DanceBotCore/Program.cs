
using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using DanceBotCore.Actors;
using DanceBotCore.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DanceBotCore
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    var config = ConfigurationFactory.ParseString(@"
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
                    "
                    );

                    var actorSystem = ActorSystem.Create("DanceBot", config);

                    services.AddSingleton(actorSystem);
                    services.AddScoped<IStepFactory, StepFactory>();
                    services.AddLogging();
                    services.AddHostedService<ActorService>();

                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();

                })
                .UseConsoleLifetime()
                .Build();

            await host.RunAsync();
        }
    }
}