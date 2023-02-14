using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramClient.Abstract;
using TelegramClient.Services;

public class ReceiverService : ReceiverServiceBase<UpdateHandler>
{
    public ReceiverService(
    ITelegramBotClient botClient,
        UpdateHandler updateHandler,
        ILogger<ReceiverServiceBase<UpdateHandler>> logger)
        : base(botClient, updateHandler, logger)
    {
    }
}