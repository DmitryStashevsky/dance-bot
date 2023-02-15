using System;
using Akka.Actor;
using Akka.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TelegramClient.Actors;

namespace TelegramClient.Services
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ActorSystem _actorSystem;
        private readonly ILogger<UpdateHandler> _logger;

        public UpdateHandler(ITelegramBotClient botClient, ActorSystem actorSystem, ILogger<UpdateHandler> logger)
        {
            _botClient = botClient;
            _actorSystem = actorSystem;
            _logger = logger;
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogError("HandleError: {ErrorMessage}", ErrorMessage);

            return Task.CompletedTask;
        }

        public Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var target = _actorSystem.ActorOf<MessageReceive>();
            target.Tell(update.Message.Chat.Id);
            return Task.CompletedTask;
        }
    }
}

