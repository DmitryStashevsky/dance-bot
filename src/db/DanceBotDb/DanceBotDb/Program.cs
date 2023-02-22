
using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using DanceBotDb.Common;
using DanceBotDb.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DanceBotDb
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
                                        port = 8083
                                        hostname = 0.0.0.0
                                        public-hostname = localhost
                                    }
                                }
                            }
                        "
                    );

                    services.AddSingleton<IDbConfiguration, DbConfiguration>();
                    services.AddScoped<IDbContext, DbContext>();

                    var actorSystem = ActorSystem.Create("DanceBot", config);
                    services.AddSingleton(actorSystem);

                    services.AddLogging();
                    services.AddHostedService<ActorService>();

                })
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false);
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