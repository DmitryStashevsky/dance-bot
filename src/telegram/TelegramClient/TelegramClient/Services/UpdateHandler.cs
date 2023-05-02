using System;
using Akka.Actor;
using Akka.Configuration;
using DanceBotShared.Common;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramClient.Actors;
using TelegramClient.Handlers;

namespace TelegramClient.Services
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ActorSystem _actorSystem;
        private readonly ILogger<UpdateHandler> _logger;

        public UpdateHandler(
            ITelegramBotClient botClient,
            ActorSystem actorSystem,
            ILogger<UpdateHandler> logger)
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
            var handler = update switch
            {
                { Message: { } message } => BotOnMessageReceived(message),
                { CallbackQuery: { } callbackQuery } => BotOnCallbackQueryReceived(callbackQuery)
            };

            return Task.CompletedTask;
        }

        //TODO remove task
        private Task BotOnMessageReceived(Message message)
        {
            var target = _actorSystem.ActorSelection($"/user/{MessageReceive.ActorName}");
            target.Tell(new MessageContext
            {
                ChatId = message.Chat.Id,
                UserName = message.From.Username,
                Message = message.Text,
                Language = message.From.LanguageCode
            });

            return Task.CompletedTask;
        }

        private Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
        {
            var target = _actorSystem.ActorSelection($"/user/{MessageReceive.ActorName}");
            target.Tell(new MessageContext
            {
                ChatId = callbackQuery.Message.Chat.Id,
                CallbackId = callbackQuery.Message.MessageId,
                UserName = callbackQuery.Message.From.Username,
                Message = callbackQuery.Message.Text,
                Language = callbackQuery.Message.From.LanguageCode
            });

            return Task.CompletedTask;
        }
    }
}

