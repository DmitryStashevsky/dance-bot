
using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Hosting;
using Akka.DependencyInjection;
using Akka.Hosting;
using Akka.Remote.Hosting;
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
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddAkka("dance-bot", (builder, provider) =>
                    {
                        builder
                            .AddHoconFile("core.hocon", HoconAddMode.Prepend)
                            .AddHocon(hocon: "akka.remote.dot-netty.tcp.maximum-frame-size = 256000b", addMode: HoconAddMode.Prepend)
                            .WithRemoting(hostname: "127.0.0.1", port: 5212)
                            // Add common DevOps settings
                            .WithClustering(new ClusterOptions
                            {
                                    SeedNodes = new[] { "akka.tcp://dance-bot@localhost:16666" },
                                    Roles = new[] { "core" }
                            })
                            // instantiate actors
                            .WithActors((system, registry) =>
                            {
                                // var apiMaster = system.ActorOf(Props.Create(() => new ApiMaster()), "api");
                                // registry.Register<ApiMaster>(apiMaster);
                            });
                    });
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